﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Flats4us.Entities.Dto;
    using Flats4us.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;  

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllUsersAsync();
            return Ok(students);
        }


        [HttpGet("policy_example")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<IActionResult> OnlyStudentPolicyExample()
        {
            var students = await _studentService.GetAllUsersAsync();
            return Ok(students);
        }

        // GET: api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetUserByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");
            return Ok(student);
        }

        // DELETE: api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            bool deleted = await _studentService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound("Student not found");
            return NoContent();
        }
    }

}
