using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("User")]
    public class User
    {
        public const int MinUsernameLenght = 6;
        public const int MaxUsernameLenght = 30;

        public const int MinPasswordLenght = 6;
        public const int MaxPasswordeLenght = 30;


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(MaxUsernameLenght)]
        [MinLength(MinUsernameLenght)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(70)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$")]
        public string PasswordHash { get; set; }



        [Required]
        public string Role{ get; set; }

    }
}
