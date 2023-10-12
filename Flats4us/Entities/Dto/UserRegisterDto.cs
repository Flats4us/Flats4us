namespace Flats4us.Entities.Dto
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int Flat { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
