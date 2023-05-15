using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public enum WhatFor
    {
        Rent,
        Deposit,
        Repairs
    }

    [Table("Payment")]
    public class Payment //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public WhatFor WhatFor { get; set; }

        [Required]
        public int Price { get; set; }

        public virtual Student Student { get; set; }
        public virtual Offer Offer { get; set; }
 
    }
}
