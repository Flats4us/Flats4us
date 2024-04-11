namespace Flats4us.Entities.Dto
{
    public class PropertyWithRentOpinionDto
    {
        public int PropertyId { get; set; }
        public string Province { get; set; } 
        public ICollection<RentOpinion> RentOpinions { get; set; }
    }
}
