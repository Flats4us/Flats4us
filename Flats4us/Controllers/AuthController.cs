using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterAsync(UserRegisterDto request)
        {
            try
            {
                var user = await _userService.RegisterAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-student")]
        public async Task<ActionResult<User>> RegisterStudentAsync(StudentRegisterDto request)
        {
            try
            {
                var user = await _userService.RegisterAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-owner")]
        public async Task<ActionResult<User>> RegisterOwnerAsync(OwnerRegisterDto request)
        {
            try
            {
                var user = await _userService.RegisterAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<String>> Login(UserLoginDto request) {

            var user = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (user == null)
            {
                return BadRequest("Incorrect username or password");
            }

            string token = CreateToken(user);

            return Ok(token);    
        }


        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserInfoDto>> GetUserProfile()
        {
            // Get the user ID from the token
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            // Retrieve the user's information from the database or any other data source
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Create a UserDto object with the required properties
            var userDto = new UserInfoDto
            {
                Id = user.UserId,
                Username = user.Username,
                Role = user.Role,
            };

            return Ok(userDto);
        }



        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Add the user ID claim
                new Claim(ClaimTypes.Role, user.Role) // Add the role claim
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
