﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("PersonOpinion")]
    public class PersonOpinion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        [Required]
        public bool Check1 { get; set; }

        [Required]
        public bool Check2 { get; set; }

        [Required]
        public bool Check3 { get; set; }

        [Required]
        public bool Check4 { get; set; }

        [Required]
        public bool Check5 { get; set; }

        [Required]
        public bool Check6 { get; set; }

        [Required]
        public bool Check7 { get; set; }

        //public virtual OwnerStudent Evaluated { get; set; }

        //public virtual OwnerStudent Evaluator { get; set; }
    }
}
