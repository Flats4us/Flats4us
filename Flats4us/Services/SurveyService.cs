﻿using System.Reflection;
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
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
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

                bool isSurveyTrigger = property.IsDefined(typeof(SurveyTriggerAttribute));

                Type propertyType = property.PropertyType;
                bool isNullable = Nullable.GetUnderlyingType(propertyType) != null;
                Type underlyingType = isNullable ? Nullable.GetUnderlyingType(propertyType) : propertyType;

                if (underlyingType == typeof(bool))
                {
                    SurveyJson surveyJson = new()
                    {
                        Id = id++,
                        Name = ToCamelCase(property.Name),
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

                    for (int i = 0; i < enumValues.Length; i++)
                    {
                        enumValues[i] = ToCamelCase(enumValues[i]);
                    }

                    SurveyJson surveyJson = new()
                    {
                        Id = id++,
                        Name = ToCamelCase(property.Name),
                        Trigger = isSurveyTrigger,
                        Optional = isNullable,
                        TypeName = "RADIOBUTTON",
                        Answers = enumValues,
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
                            Name = ToCamelCase(property.Name),
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
                            Name = ToCamelCase(property.Name),
                            Trigger = isSurveyTrigger,
                            Optional = isNullable,
                            TypeName = "NUMBER",
                            Answers = new string[0]
                        };
                    }
                    json.Add(surveyJson);
                }
                else if (underlyingType == typeof(string))
                {
                    SurveyJson surveyJson;

                    SurveyNullableString nullable = (SurveyNullableString)property.GetCustomAttribute(typeof(SurveyNullableString), true);

                    if (nullable != null)
                    {
                        surveyJson = new()
                        {
                            Id = id++,
                            Name = ToCamelCase(property.Name),
                            Trigger = isSurveyTrigger,
                            Optional = true,
                            TypeName = "TEXT",
                            Answers = new string[0],
                        };
                    }
                    else
                    {
                        surveyJson = new()
                        {
                            Id = id++,
                            Name = ToCamelCase(property.Name),
                            Trigger = isSurveyTrigger,
                            Optional = false,
                            TypeName = "TEXT",
                            Answers = new string[0],
                        };
                    }
                    
                    json.Add(surveyJson);
                }
            }

            string j = JsonSerializer.Serialize(json, options);
            return j;
        }

        private static string ToCamelCase(string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }
    }
}
