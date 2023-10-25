﻿using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IUserService _ownerService;  // Assuming you have an IOwnerService interface for OwnerService.

        public OwnerController(IUserService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/<OwnerController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var owners = await _ownerService.GetAllUsersAsync();
            if (owners == null || !owners.Any())
                return NotFound("No owners found");
            return Ok(owners);
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var owner = await _ownerService.GetUserByIdAsync(id);
            if (owner == null)
                return NotFound("Owner not found");
            return Ok(owner);
        }

        

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _ownerService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound("Owner not found");
            return NoContent();
        }
    }

}
