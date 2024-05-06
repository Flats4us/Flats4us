using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult ResetDatabase()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Flats4usContext>();

                dbContext.Database.EnsureDeleted();

                if (dbContext.Database.EnsureCreated())
                {
                    DataSeeder.SeedData(dbContext);
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
