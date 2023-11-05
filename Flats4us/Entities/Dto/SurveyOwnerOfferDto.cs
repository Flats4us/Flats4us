using Flats4us.Helpers.Enums;
using Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flats4us.Entities.Dto

{
    public class SurveyOwnerOfferDto
    {
        public int SurveyOwnerOfferId { get; set; }
        public int NumberOfInhabitants { get; set; }
        public bool Smoking { get; set; }
        public bool Parties { get; set; }
        public bool Animals { get; set; }
        public Gender Gender { get; set; }

        //public virtual Owner Owner { get; set; }

        //public virtual Offer Offer { get; set; }
    }
}
