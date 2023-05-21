using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public abstract class Student : OwnerStudent
    {
        [Required]
        public int YearOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string StudentNumber { get; set; }

        [Required]
        public string University { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public virtual SurveyStudent SurveyStudent { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }

        public Student()
        {
            this.Interests = new HashSet<Interest>();
        }
    }
}
