using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SurveyStudentController : ControllerBase
    {
        private readonly ILogger<SurveyStudentController> _logger;
        private readonly ISurveyStudentService _surveyStudentService;

        public SurveyStudentController(
            ILogger<SurveyStudentController> logger,
            ISurveyStudentService surveyStudentService
        )
        {
            _logger = logger;
            _surveyStudentService = surveyStudentService;
        }

        [HttpGet]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudents()
        {
            _logger.LogInformation("Getting SurveyStudent");
            var surveyStudent = await _surveyStudentService.GetAllAsync();

            return Ok(surveyStudent);
        }


        [HttpGet("{id}")]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudentById(int id)
        {
            _logger.LogInformation("Getting SurveyStudent by ID");
            var surveyStudent = await _surveyStudentService.GetById(id);

            return Ok(surveyStudent);
        }

        [HttpPost]
        //[Route("GetSurveyStudent")]
        public async Task<IActionResult> PostSurveyStudent(SurveyStudentPost surveyStudent)
        {
            _logger.LogInformation("Getting SurveyStudent");
            await _surveyStudentService.Post(new SurveyStudent
            {
                Party = surveyStudent.Party,
                Tidiness = surveyStudent.Tidiness,
                Smoking = surveyStudent.Smoking,
                Sociability = surveyStudent.Sociability,
                Animals = surveyStudent.Animals,
                Vegan = surveyStudent.Vegan,
                LookingForRoommate = surveyStudent.LookingForRoommate,
                MaxNumberOfRoommates = surveyStudent.MaxNumberOfRoommates,
                RoommateGender = surveyStudent.RoommateGender,
                MinRoommateAge = surveyStudent.MinRoommateAge,
                //Student = surveyStudent.Student
            });

            return Ok();
        }
    }
}
