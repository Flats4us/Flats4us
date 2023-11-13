using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public abstract class OwnerStudentService : UserService
    {
        protected OwnerStudentService(Flats4usContext context) : base(context)
        {
        }
        protected OwnerStudent PopulateOwnerStudentFieldsFromDto(OwnerStudent ownerStudent, OwnerStudentRegisterDto request)
        {
            // Call base method to populate common User fields
            ownerStudent = (OwnerStudent)PopulateCommonFieldsFromDto(ownerStudent, request);

            // Populate OwnerStudent specific fields
            ownerStudent.PhotoPath = request.PhotoPath;
            ownerStudent.ActivityStatus = request.ActivityStatus;
            ownerStudent.DocumentPath = request.DocumentPath;
            ownerStudent.DocumentType = request.DocumentType;
            ownerStudent.VerificationStatus = request.VerificationStatus;
            ownerStudent.DocumentExpireDate = request.DocumentExpireDate;

            return ownerStudent;
        }

        protected abstract override User CreateUserFromDto(UserRegisterDto request);


    }
}