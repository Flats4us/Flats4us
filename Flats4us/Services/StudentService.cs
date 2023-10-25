using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services
{
    public class StudentService : OwnerStudentService
    {
        public StudentService(Flats4usContext context) : base(context)
        {
        }

        protected override User CreateUserFromDto(UserRegisterDto request)
        {
            var studentDto = request as StudentRegisterDto;
            if (studentDto == null) throw new ArgumentException("Invalid DTO for student registration");

            // Populate properties from OwnerStudent
            //var student = base.CreateUserFromDto(studentDto) as Student;
            var student = new Student();
            student = (Student)PopulateCommonFieldsFromDto(student, studentDto);
            student = (Student)PopulateOwnerStudentFieldsFromDto(student, studentDto);
            student.Role = "Student";

            // Fields specific to Student (like Interests, Meetings etc.) can be populated here if they're part of the DTO
            // For instance: 
            // student.Interests = studentDto.SomeField; 

            // Collections are initialized in the constructor, so no need to handle them here unless you're populating them from the DTO

            student.YearOfBirth = studentDto.YearOfBirth;
            
            student.StudentNumber = studentDto.StudentNumber;
            student.University = studentDto.University;
            student.Facebook = studentDto.Facebook;
            student.Twitter = studentDto.Twitter;
            student.Instagram = studentDto.Instagram;
            student.RoommatesStatus = studentDto.RoommatesStatus;
            student.IsTenant = studentDto.IsTenant;

            return student;
        }
    }

}
