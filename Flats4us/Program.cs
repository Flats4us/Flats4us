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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Flats4usContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Flats4usConn"));
    }, 
    ServiceLifetime.Scoped
);

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddTransient<IOpenStreetMapService, OpenStreetMapService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddTransient<IBackgroundJobService, BackgroundJobService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITechnicalProblemService, TechnicalProblemService>();

builder.Services.AddControllers();

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

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Flats4usConn")));

builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<Flats4usContext>();

    if (dbContext.Database.EnsureCreated())
    {
        DataSeeder.SeedData(dbContext);
    }
}

app.UseCors(options => options.AllowAnyOrigin());

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
