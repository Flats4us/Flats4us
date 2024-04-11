﻿using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto
{
    public class RentDto
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public ICollection<RentOpinionDto> RentOpinion { get; set; }

    }
}
