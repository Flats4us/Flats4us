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
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasData(
            new Payment {Id = 1, WhatFor = WhatFor.Rent, Price = 1500 },
            new Payment {Id = 2, WhatFor = WhatFor.Deposit, Price = 2000 },
            new Payment {Id = 3, WhatFor = WhatFor.Rent, Price = 1200 },
            new Payment {Id = 4, WhatFor = WhatFor.Repairs, Price = 500 },
            new Payment {Id = 5, WhatFor = WhatFor.Rent, Price = 1000 },
            new Payment {Id = 6, WhatFor = WhatFor.Deposit, Price = 1500 });

        }
    }
}
