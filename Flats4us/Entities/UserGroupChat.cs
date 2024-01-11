﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class UserGroupChat
    {
        

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupChatId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual GroupChat GroupChat { get; set; }
    }

}