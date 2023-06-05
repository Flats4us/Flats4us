using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Text.Json;
using Helpers;

namespace Flats4us.Services
{
    public class SurveyStudentService : ISurveyStudentService
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        public async Task<string> MakingSurvey(Type type)
        {

            var attributes = type.GetProperties();

            int id = 1;
            List<SurveyJson> json = new();
            foreach (PropertyInfo property in attributes)
            {
                if (property.IsDefined(typeof(SurveyIgnoreAttribute)))
                {
                    continue;
                }

                if (property.PropertyType == typeof(bool))
                {
                    SurveyJson surveyJson = new()
                    {
                        id = id++,
                        name = property.Name,
                        type_name = "SWITCH",
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
                        name = property.Name,
                        type_name = "RADIOBUTTON",
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
                            name = property.Name,
                            type_name = "SLIDER",
                            answers = new string[] {slider.MinimumValue.ToString(), slider.MaximumValue.ToString() }
                        };
                    }
                    else
                    {
                        surveyJson = new()
                        {
                            id = id++,
                            name = property.Name,
                            type_name = "NUMBER",
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
