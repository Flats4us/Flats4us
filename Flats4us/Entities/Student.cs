﻿using Flats4us.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flats4us.Entities
{
    public class Student : OwnerStudent
    {
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string StudentNumber { get; set; }

        [Required]
        public string University { get; set; }

        [Required]
        public string Links { get; set; }

        public RoommatesStatus? RoommatesStatus { get; set; }

        [Required]
        public bool IsTenant { get; set; }

        public virtual SurveyStudent SurveyStudent { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<OpinionStudentStudent> ReceivedStudentStudentOpinions { get; set; }
        public virtual ICollection<OpinionStudentStudent> IssuedStudentStudentOpinions { get; set; }
        public virtual ICollection<OpinionOwnerStudent> ReceivedOwnertStudentOpinions { get; set; }
        public virtual ICollection<OpinionStudentOwner> IssuedStudentOwnerOpinions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Argument> Arguments { get; set; }
        public virtual ICollection<Rent> RoommateInRents { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        public virtual ICollection<OfferInterest> OfferInterests { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }

        public Student()
        {
            this.Interests = new HashSet<Interest>();
            this.Meetings = new HashSet<Meeting>();
            this.ReceivedStudentStudentOpinions= new HashSet<OpinionStudentStudent>();
            this.IssuedStudentStudentOpinions= new HashSet<OpinionStudentStudent>();
            this.ReceivedOwnertStudentOpinions = new HashSet<OpinionOwnerStudent>();
            this.IssuedStudentOwnerOpinions = new HashSet<OpinionStudentOwner>();
            this.Payments = new HashSet<Payment>();
            this.Arguments = new HashSet<Argument>();
            this.RoommateInRents= new HashSet<Rent>();
            this.Rents = new HashSet<Rent>();
            this.OfferInterests = new HashSet<OfferInterest>();
            this.Chats = new HashSet<Chat>();
        }
    }
}
