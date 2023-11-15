using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Flats4us.Services.Interfaces;
using Flats4us.Services;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOwnerService _ownerService;
        private readonly IStudentService _studentService;

        public AuthController(IConfiguration configuration, IOwnerService ownerService, IStudentService studentService)
        {
            _configuration = configuration;
            _ownerService = ownerService;
            _studentService = studentService;
            
        }

        [HttpPost("register/Student")]
        public async Task<ActionResult<User>> RegisterStudentAsync([FromForm] StudentRegisterDto request)
        {
            try
            {
                await _studentService.RegisterAsync(request);
                return Ok("User added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register/Owner")]
        public async Task<ActionResult<User>> RegisterOwnerAsync([FromForm] OwnerRegisterDto request)
        {
            try
            {

                var user = await _ownerService.RegisterAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> LoginStudent([FromForm] UserLoginDto request) {
            var user = await _studentService.AuthenticateAsync(request.Username, request.Password);
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

            IUserService userService;
            User basicUser;

            // Try fetching with StudentService
            basicUser = await _studentService.GetUserByIdAsync(userId);

            if (basicUser == null)
            {
                // Try fetching with OwnerService
                basicUser = await _ownerService.GetUserByIdAsync(userId);
            }

            if (basicUser == null)
            {
                return NotFound();
            }

            var userDto = new UserInfoDto
            {
                Id = basicUser.UserId,
                Username = basicUser.Username,
            };

            return Ok(userDto);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Add the user ID claim
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
