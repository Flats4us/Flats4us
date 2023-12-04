using System.ComponentModel.DataAnnotations;
using Flats4us.Entities.Dto;
using Newtonsoft.Json;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ValidInterestJsonAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var jsonString = value as string;

        try
        {
            var interestList = JsonConvert.DeserializeObject<List<InterestDto>>(jsonString);
            return interestList != null;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}