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

        public virtual ICollection<OpinionStudentStudent> ReceivedStudentStudentOpinions { get; set; }
        public virtual ICollection<OpinionStudentStudent> IssuedStudentStudentOpinions { get; set; }
        public virtual ICollection<OpinionOwnerStudent> ReceivedOwnertStudentOpinions { get; set; }
        public virtual ICollection<OpinionStudentOwner> IssuedStudentOwnerOpinions { get; set; }

        public Student()
        {
            this.Interests = new HashSet<Interest>();
            this.ReceivedStudentStudentOpinions= new HashSet<OpinionStudentStudent>();
            this.IssuedStudentStudentOpinions= new HashSet<OpinionStudentStudent>();
            this.ReceivedOwnertStudentOpinions = new HashSet<OpinionOwnerStudent>();
            this.IssuedStudentOwnerOpinions = new HashSet<OpinionStudentOwner>();
        }
    }
}
