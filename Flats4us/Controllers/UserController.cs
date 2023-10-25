using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;  

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || !users.Any())
                return NotFound("No users found");
            return Ok(users);
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

       

       

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound("User not found");
            return NoContent();
        }
    }
}

