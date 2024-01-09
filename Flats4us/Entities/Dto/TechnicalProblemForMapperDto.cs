﻿using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class TechnicalProblemForMapperDto
    {
        [Required]
        public TechnicalProblemEnum Kind { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Solved { get; set; }

        public int UserId { get; set; }
    }
}
