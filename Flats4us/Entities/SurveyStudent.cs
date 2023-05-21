using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class SurveyStudent
    {
        [ForeignKey("Student")]
        public int SurveyStudentId { get; set; }


        public int test { get; set; }

        public virtual Student Student { get; set; }
    }
}
