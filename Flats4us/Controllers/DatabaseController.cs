﻿using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/database")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost("reset")]
        [SwaggerOperation(
            Summary = "Resets database. This may take some time."
        )]
        public async Task<IActionResult> ResetDatabase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Flats4usContext>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                dbContext.Database.EnsureDeleted();

                if (dbContext.Database.EnsureCreated())
                {
                    GlobalConfiguration.Configuration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage(configuration.GetConnectionString("Flats4usConn"));

                    var scopeFactory = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
                    HangfireSetup.ConfigureJobs(scopeFactory);

                    await DataSeeder.SeedDataAsync(dbContext, configuration);

                    return Ok(new OutputDto<string>("Database reset successfully"));
                }
                else
                {
                    return BadRequest(new OutputDto<string>("Database reset failed. Contact with administrator"));
                }
            }
        }
    }
}
