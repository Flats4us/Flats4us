using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Text.Json;
using Helpers;
using System.Xml;
using System.Text.Encodings.Web;
using Flats4us.Services.Interfaces;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public class SurveyService : ISurveyService
    {
        

        public readonly Flats4usContext _context;

        public SurveyService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<SurveyStudent>> GetAllSurveyStudentsAsync()
        {
            return await _context.StudentSurveys.ToListAsync();
        }

        public async Task<SurveyStudent> GetSurveyStudentById(int id)
        {
            return await _context.StudentSurveys.FirstAsync(x => x.SurveyStudentId == id);
        }

        public async Task AddSurveyStudentAsync(SurveyStudentDto input)
        {
            var surveyStudent = new SurveyStudent
            {
                Party = input.Party,
                Tidiness = input.Tidiness,
                Smoking = input.Smoking,
                Sociability = input.Sociability,
                Animals = input.Animals,
                Vegan = input.Vegan,
                LookingForRoommate = input.LookingForRoommate,
                MaxNumberOfRoommates = input.MaxNumberOfRoommates,
                RoommateGender = input.RoommateGender,
                MinRoommateAge = input.MinRoommateAge,
                StudentId = input.StudentId,
            };

            await _context.StudentSurveys.AddAsync(surveyStudent);
            await _context.SaveChangesAsync();
        }




        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };
        public async Task<string> MakingSurvey(Type type, string title, string lang)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load($"Lang/{lang}.xml");
               
            var attributes = type.GetProperties();

            int id = 1;
            List<SurveyJson> json = new();
            foreach (PropertyInfo property in attributes)
            {
                if (property.IsDefined(typeof(SurveyIgnoreAttribute)))
                {
                    continue;
                }

                XmlNode stringNode = xmlDoc.SelectSingleNode($"/resources/string[@name='{property.Name}']");

                string content;

                if (stringNode != null)
                {
                    content = stringNode.InnerText;
                }
                else
                {
                    content = "notFound";
                }

                if (property.PropertyType == typeof(bool))
                {
                    SurveyJson surveyJson = new()
                    {
                        id = id++,
                        title = title,
                        content = content,
                        typeName = "SWITCH",
                        answers = new string[0]
                    };

                    json.Add(surveyJson);
                }
                else if (property.PropertyType.IsEnum)
                {
                    string[] enumValues = Enum.GetNames(property.PropertyType);

                    SurveyJson surveyJson = new()
                    {
                        id = id++,
                        title = title,
                        content = content,
                        typeName = "RADIOBUTTON",
                        answers = enumValues,
                    };

                    json.Add(surveyJson);
                }
                else if (property.PropertyType == typeof(int))
                {
                    SurveyJson surveyJson;

                    SurveySliderAttribute slider = (SurveySliderAttribute)property.GetCustomAttribute(typeof(SurveySliderAttribute), true);
                    if (slider != null)
                    {
                        surveyJson = new()
                        {
                            id = id++,
                            title = title,
                            content = content,
                            typeName = "SLIDER",
                            answers = new string[] {slider.MinimumValue.ToString(), slider.MaximumValue.ToString() }
                        };
                    }
                    else
                    {
                        surveyJson = new()
                        {
                            id = id++,
                            title = title,
                            content = content,
                            typeName = "NUMBER",
                            answers = new string[0]
                        };
                    }
                    json.Add(surveyJson);
                }
            }

            string j = JsonSerializer.Serialize(json, options);
            return j;
        }
    }
}
