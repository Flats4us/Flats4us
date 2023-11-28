using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public abstract class OwnerStudentService : UserService
    {
        protected OwnerStudentService(Flats4usContext context, IMapper mapper) : base(context, mapper)
        {
        }
        protected OwnerStudent PopulateOwnerStudentFieldsFromDto(OwnerStudent ownerStudent, OwnerStudentRegisterDto request)
        {
            // Call base method to populate common User fields
            ownerStudent = (OwnerStudent)PopulateCommonFieldsFromDto(ownerStudent, request);

            // Populate OwnerStudent specific fields
            ownerStudent.DocumentType = request.DocumentType;
            ownerStudent.DocumentExpireDate = request.DocumentExpireDate;
            
            return ownerStudent;
        }

        protected abstract User CreateUserFromDto(OwnerStudentRegisterDto request);
    }
}