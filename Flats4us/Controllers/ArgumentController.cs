﻿using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArgumentController : ControllerBase
    {
        private readonly ILogger<ArgumentController> _logger;
        private readonly IArgumentService _argumentService;

        public ArgumentController(
            ILogger<ArgumentController> logger,
            IArgumentService argumentService
        )
        {
            _logger = logger;
            _argumentService = argumentService;
        }

        [HttpGet("get_argument")]
        public async Task<IActionResult> GetArguments()
        {
            _logger.LogInformation("Geting Arguments");
            var arguments = await _argumentService.GetAllArgumentsAsync();

            return Ok(arguments);
        }

        [HttpGet("get_id_argument")]
        public async Task<IActionResult> GetArgumentById(int id)
        {
            _logger.LogInformation("Geting Argument by Id");
            var argument = await _argumentService.GetArgumentById(id);

            return Ok(argument);
        }

        [HttpPost("post_argument")]
        public async Task<IActionResult> PostArgument(ArgumentDto input)
        {
            try
            {
                _logger.LogInformation("Posting Argument");
                await _argumentService.AddArgumentAsync(input);
                return Ok("Argument Added");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: {input}");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpPut("put_status_argument")]
        public async Task<IActionResult> PutArgument(int id, ArgumentStatus status)
        {
            try
            {
                _logger.LogInformation("Put Argument");
                await _argumentService.EditStatusArgumentAsync(id, status);
                return Ok("Argument Added");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Editing argument");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpGet("get_filtered_argument")]
        public async Task<IActionResult> GetFilteredArguments()
        {
            _logger.LogInformation("Geting Arguments");
            var arguments = await _argumentService.GetOngoingArgumentsAsync();

            return Ok(arguments);
        }


    }
}
