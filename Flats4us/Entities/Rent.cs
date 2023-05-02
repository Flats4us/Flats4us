using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("Rent")]
    public class Rent //not abstract
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int? LengthInMonths { get; set; }

        [Required]
        public string? ContractInformations { get; set; }

        public virtual RentOpinion RentOpinion { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual ICollection<Student> OtherTenants { get; set; }

        public Rent()
        {
            OtherTenants = new HashSet<Student>();
        }
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rent>().HasData(
            new Payment { Id = 1, WhatFor = WhatFor.Rent, Price = 1000 },
            new Payment { Id = 2, WhatFor = WhatFor.Deposit, Price = 500 },
            new Payment { Id = 3, WhatFor = WhatFor.Repairs, Price = 250 });
        }
    }
}
