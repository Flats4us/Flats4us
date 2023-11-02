using Flats4us.Entities;
using Flats4us.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Flats4usContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Flats4usConn")));



//builder.Services.AddTransient<ITenantService, TenantService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



//builder.Services.AddScoped<IUserService, StudentService>();
//builder.Services.AddScoped<IUserService, OwnerService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<OwnerService>();

builder.Services.AddScoped<Func<string, IUserService>>(serviceProvider => key =>
{
    switch (key)
    {
        case "Student":
            return serviceProvider.GetService<StudentService>();
        case "Owner":
            return serviceProvider.GetService<OwnerService>();
        default:
            throw new KeyNotFoundException(); 
    }
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrTenantOnly", policy =>
    {
        policy.RequireRole("Admin", "Tenant");
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
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod());
});

using var scope = builder.Services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<Flats4usContext>();
dbContext.Database.Migrate();

var app = builder.Build();
app.UseStaticFiles();   // for swagger

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}




app.UseCors(options => options.AllowAnyOrigin());

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
