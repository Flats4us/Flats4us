using System.ComponentModel.DataAnnotations;
using Flats4us.Entities.Dto;
using Newtonsoft.Json;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ValidEquipmentJsonAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        var jsonString = value as string;

        try
        {
            var equipmentList = JsonConvert.DeserializeObject<List<EquipmentDto>>(jsonString);
            return equipmentList != null;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}