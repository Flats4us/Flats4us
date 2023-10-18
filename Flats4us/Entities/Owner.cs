﻿using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities
{
    public class Owner : OwnerStudent
    {
        [Required]
        public string BankAccount { get; set; }

        public virtual ICollection<OpinionOwnerStudent> IssuedOwnerStudentOpinions { get; set; }
        public virtual ICollection<OpinionStudentOwner> ReceivedStudentOwnerOpinions { get; set; }
        public virtual ICollection<SurveyOwnerOffer> OwnerOfferSurveys { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Property> Properties { get; set; }

        public Owner()
        {
            this.IssuedOwnerStudentOpinions = new HashSet<OpinionOwnerStudent>();
            this.ReceivedStudentOwnerOpinions = new HashSet<OpinionStudentOwner>();
            this.OwnerOfferSurveys = new HashSet<SurveyOwnerOffer>();
            this.Chats = new HashSet<Chat>();
            this.Properties = new HashSet<Property>();
        }
    }
}
