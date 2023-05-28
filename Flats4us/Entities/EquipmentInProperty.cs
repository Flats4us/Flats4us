﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    [Table("EquipmentInProperty")]
    public class EquipmentInProperty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        public virtual Property Property { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
