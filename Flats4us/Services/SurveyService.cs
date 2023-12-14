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

namespace Flats4us.Services
{
    public class SurveyService : ISurveyService
    {
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

                string content = stringNode != null ? stringNode.InnerText : "notFound";

                bool isSurveyTrigger = property.IsDefined(typeof(SurveyTriggerAttribute));

                Type propertyType = property.PropertyType;
                bool isNullable = Nullable.GetUnderlyingType(propertyType) != null;
                Type underlyingType = isNullable ? Nullable.GetUnderlyingType(propertyType) : propertyType;

                if (underlyingType == typeof(bool))
                {
                    SurveyJson surveyJson = new()
                    {
                        Id = id++,
                        Title = title,
                        Content = content,
                        Trigger = isSurveyTrigger,
                        Optional = isNullable,
                        TypeName = "SWITCH",
                        Answers = new string[0]
                    };

                    json.Add(surveyJson);
                }
                else if (underlyingType.IsEnum || (isNullable && Nullable.GetUnderlyingType(propertyType)?.IsEnum == true))
                {
                    string[] enumValues = Enum.GetNames(underlyingType);

                    string[] translatedEnumValues = new string[enumValues.Length];

                    for (int i = 0; i < enumValues.Length; i++)
                    {
                        string enumKey = $"{underlyingType.Name}_{enumValues[i]}";

                        XmlNode enumStringNode = xmlDoc.SelectSingleNode($"/resources/string[@name='{enumKey}']");
                        translatedEnumValues[i] = enumStringNode != null ? enumStringNode.InnerText : enumValues[i];
                    }

                    SurveyJson surveyJson = new()
                    {
                        Id = id++,
                        Title = title,
                        Content = content,
                        Trigger = isSurveyTrigger,
                        Optional = isNullable,
                        TypeName = "RADIOBUTTON",
                        Answers = translatedEnumValues,
                    };

                    json.Add(surveyJson);
                }
                else if (underlyingType == typeof(int))
                {
                    SurveyJson surveyJson;

                    SurveySliderAttribute slider = (SurveySliderAttribute)property.GetCustomAttribute(typeof(SurveySliderAttribute), true);
                    if (slider != null)
                    {
                        surveyJson = new()
                        {
                            Id = id++,
                            Title = title,
                            Content = content,
                            Trigger = isSurveyTrigger,
                            Optional = isNullable,
                            TypeName = "SLIDER",
                            Answers = new string[] {slider.MinimumValue.ToString(), slider.MaximumValue.ToString() }
                        };
                    }
                    else
                    {
                        surveyJson = new()
                        {
                            Id = id++,
                            Title = title,
                            Content = content,
                            Trigger = isSurveyTrigger,
                            Optional = isNullable,
                            TypeName = "NUMBER",
                            Answers = new string[0]
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
