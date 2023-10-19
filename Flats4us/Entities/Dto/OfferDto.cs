﻿using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class OfferDto
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OfferStatus OfferStatus { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Decription { get; set; }

        // In Months 
        [Required]
        public int RentalPeriod { get; set; }

        [Required]
        public int NumberOfInterested { get; set; }

        [Required]
        public string Regulations { get; set; }

        [Required]
        public PropertyDto Property { get; set; }

        [Required]
        public OwnerStudentDto Owner { get; set; }

        [Required]
        public SurveyOwnerOffer SurveyOwnerOffer { get; set; }
    }
}
