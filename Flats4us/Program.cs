using Flats4us.Entities;
using Flats4us.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Microsoft.OpenApi.Models;
using Hangfire;
using Flats4us.Helpers;
using AutoMapper;
using Flats4us.Helpers.Enums;
using Microsoft.Extensions.FileProviders;
using Flats4us.Hubs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Flats4usContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Flats4usConn"));
    }, 
    ServiceLifetime.Scoped
);


builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IOpenStreetMapService, OpenStreetMapService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddTransient<IBackgroundJobService, BackgroundJobService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMatcherService, MatcherService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<ITechnicalProblemService, TechnicalProblemService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IGroupChatService, GroupChatService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddSingleton(new AppInfo { 
    CommitHash = Environment.GetEnvironmentVariable("COMMIT_HASH") ?? "notFound", 
    CommitDate = Environment.GetEnvironmentVariable("COMMIT_DATE") ?? "notFound"
});

builder.Services.AddSingleton<GitHeadersFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GitHeadersFilter));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // Note: Must be lowercase
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/chatHub"))) // Make sure this matches your SignalR hub route
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Moderator", policy =>
    {
        policy.RequireRole("Moderator");
        policy.RequireClaim("VerificationStatus", VerificationStatus.Verified.ToString());
    });

    options.AddPolicy("Student", policy =>
    {
        policy.RequireRole("Student");
    });

    options.AddPolicy("Owner", policy =>
    {
        policy.RequireRole("Owner");
    });

    options.AddPolicy("VerifiedStudent", policy =>
    {
        policy.RequireRole("Student");
        policy.RequireClaim("VerificationStatus", VerificationStatus.Verified.ToString());
    });

    options.AddPolicy("VerifiedOwner", policy =>
    {
        policy.RequireRole("Owner");
        policy.RequireClaim("VerificationStatus", VerificationStatus.Verified.ToString());
    });

    options.AddPolicy("VerifiedOwnerOrStudent", policy =>
    {
        policy.RequireRole("Owner", "Student");
        policy.RequireClaim("VerificationStatus", VerificationStatus.Verified.ToString());
    });

    options.AddPolicy("RegisteredUser", policy =>
    {
        policy.RequireRole("Owner", "Student", "Moderator");
    });
});

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSignalR();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("http://localhost:4200") // Add here the specific origins
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()); // Allow credentials
});

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Flats4usConn")));

builder.Services.AddHangfireServer();


var firebaseApp = FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("onlyflats-410722-firebase-adminsdk-2t3aj-e8b09bb560.json")
});
builder.Services.AddSingleton(firebaseApp);


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<Flats4usContext>();

    if (dbContext.Database.EnsureCreated())
    {
        DataSeeder.SeedData(dbContext);
    }
}

//app.UseCors(options => options.AllowAnyOrigin());
app.MapHub<ChatHub>("/chatHub");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
HangfireSetup.ConfigureJobs(scopeFactory);

app.MapControllers();

app.Run();
