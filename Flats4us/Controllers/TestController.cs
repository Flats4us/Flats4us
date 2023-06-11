﻿using Flats4us.Entities;
using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _surveyService;
        private readonly ILogger<TestController> _logger;
        //private readonly ISurveyStudentService _surveyStudentService;
        //private readonly ISurveyOwnerOfferService _surveyOwnerOfferService;



        public TestController(ITestService surveyService,
                                ILogger<TestController> logger
                                //ISurveyStudentService surveyStudentService,
                                //ISurveyOwnerOfferService surveyOwnerOfferService
                                )
        {
            _surveyService = surveyService;
            _logger = logger;
            //_surveyStudentService = surveyStudentService;
            //_surveyOwnerOfferService= surveyOwnerOfferService;
        }


        // GET: api/Test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting students list");
            var students = await _surveyService.GetAllStudentsAsync();

            return Ok(students);
        }

        // GET api/Survey/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Getting survey - studentId: {id}", id);
            var survey = _surveyService.GetSurveyOfStudentById(id);
            if (survey is null)
                return BadRequest("Survey not found");
            else
                return Ok(survey);
        }

/*
        // GET: ankieta_surveyStudent 
        [HttpGet]
        [Route("GetSurveyStudent")]
        public async Task<IActionResult> GetSurveyStudents()
        {
            _logger.LogInformation("Getting SurveyStudent");
            var surveyStudent = await _surveyStudentService.MakingSurvey(typeof(SurveyStudent));
            

            return Ok(surveyStudent);
        }
*/

        // GET: ankieta_surveyOwnerOffer 
        /*[HttpGet]
        [Route("GetSurveyOwnerOffer")]
        public async Task<IActionResult> GetSurveyOwnerOffer()
        {
            _logger.LogInformation("Getting SurveyOwnerOffer");
            var surveyOwnerOffer = await _surveyOwnerOfferService.MakingSurveyOwnerOffer(typeof(SurveyOwnerOffer));
            

            return Ok(surveyOwnerOffer);
        }*/
    }
}
