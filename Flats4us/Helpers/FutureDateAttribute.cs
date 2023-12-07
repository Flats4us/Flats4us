using System.ComponentModel.DataAnnotations;

namespace Flats4us.Helpers
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value).Date;
            return d > DateTime.Now.Date;
        }
    }
}
