﻿using Flats4us.Entities;
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
using Flats4us.Helpers.Exceptions;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register/Student")]
        [SwaggerOperation(
            Summary = "Registers new student"
        )]
        public async Task<ActionResult> RegisterStudentAsync([FromForm] StudentRegisterDto request)
        {
            try
            {
                await _userService.RegisterStudentAsync(request);
                return Ok("Registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register/Owner")]
        [SwaggerOperation(
            Summary = "Registers new owner"
        )]
        public async Task<ActionResult> RegisterOwnerAsync([FromForm] OwnerRegisterDto request)
        {
            try
            {
                await _userService.RegisterOwnerAsync(request);
                return Ok("Registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Logs the user in, returns a token"
        )]
        public async Task<ActionResult> Login([FromBody] UserLoginDto request)
        {
            try
            {
                var token = await _userService.AuthenticateAsync(request.Email, request.Password);

                return Ok(token);    
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
