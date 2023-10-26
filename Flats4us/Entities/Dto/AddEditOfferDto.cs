﻿using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class AddEditOfferDto
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Regulations { get; set; }

        #region SurveyOwnerOffer

        [Required]
        public bool Smoking { get; set; }

        [Required]
        public bool Parties { get; set; }

        [Required]
        public bool Animals { get; set; }

        [Required]
        public Gender Gender { get; set; }

        #endregion
    }
}
