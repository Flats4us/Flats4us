namespace Flats4us.Entities
{
    public enum Department
    {
        dept1,
        dept2
    }

    public class Moderator
    {
        public DateTime HireDate { get; set; }

        public Department Department { get; set; }
    }
}
