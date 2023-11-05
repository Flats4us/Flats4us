using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services
{
    public class OwnerService : OwnerStudentService
    {
        public OwnerService(Flats4usContext context) : base(context)
        {
        }

        protected override User CreateUserFromDto(UserRegisterDto request)
        {
            var ownerDto = request as OwnerRegisterDto; // Assuming you have a specific DTO for Owner registration.
            if (ownerDto == null) throw new ArgumentException("Invalid DTO for owner registration");

            var owner = new Owner();
            PopulateOwnerStudentFieldsFromDto(owner, ownerDto); // This will populate the fields common to OwnerStudent.
            owner.Role = "Owner";

            // Fields specific to Owner can be populated here if they're part of the DTO
            owner.BankAccount = ownerDto.BankAccount;
            // ... other Owner-specific fields ...

            return owner;
        }
    }
}
