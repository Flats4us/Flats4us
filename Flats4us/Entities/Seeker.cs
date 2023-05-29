namespace Flats4us.Entities
{
    public class Seeker : Student
    {
        public virtual ICollection<OfferInterest> OfferInterests { get; set; }

        public Seeker()
        {
            this.OfferInterests = new HashSet<OfferInterest>();
        }
    }
}
