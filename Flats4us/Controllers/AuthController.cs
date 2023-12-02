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

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOwnerService _ownerService;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IOwnerService ownerService, IStudentService studentService, IUserService userService)
        {
            _configuration = configuration;
            _ownerService = ownerService;
            _studentService = studentService;
            _userService = userService;
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
        public async Task<ActionResult<String>> Login([FromForm] UserLoginDto request)
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
