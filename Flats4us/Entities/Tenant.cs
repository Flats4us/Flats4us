﻿using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public enum RoommatesStatus
    {
        Roommate,
        Alone
    }

    public class Tenant : Student
    {
        [Required]
        public RoommatesStatus RoommatesStatus { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
