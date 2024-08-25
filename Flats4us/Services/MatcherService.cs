using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using Flats4us.Helpers;
using System.Text;

namespace Flats4us.Services
{
    public class MatcherService : IMatcherService
    {
        public readonly Flats4usContext _context;
        public readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public MatcherService(
            Flats4usContext context,
            IConfiguration configuration,
            IMapper mapper,
            INotificationService notificationService)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<List<StudentForMatcherDto>> GetMatchByStudentId(int requestUserId)
        {
            var matchingIds = await _context.Matcher
                .Where(m => (m.Student1Id == requestUserId || m.Student2Id == requestUserId)
                            && m.IsStudent1Interested == true && m.IsStudent2Interested == true)
                .Select(m => m.Student1Id == requestUserId ? m.Student2Id : m.Student1Id)
                .Distinct()
                .ToListAsync();

            var matchingStudents = await _context.Students
                .Include(potential => potential.Interests)
                .Include(potential => potential.ProfilePicture)
                .Where(s => matchingIds.Contains(s.UserId) && s.UserId != requestUserId)
                .Select(potential => _mapper.Map<StudentForMatcherDto>(potential))
                .ToListAsync();

            var chats = await _context.Chats
                .Where(c => c.User1Id == requestUserId || c.User2Id == requestUserId)
                .Distinct()
                .ToListAsync();

            foreach (var student in matchingStudents)
            {
                var chat = chats.First(c => c.User1Id == student.UserId || c.User2Id == student.UserId);

                if (chat != null) student.ChatId = chat.ChatId;
            }

            return matchingStudents;
        }

        public async Task<List<StudentForMatcherDto>> GetPotentialRoommateAsync(int studentId)
        {
            var requestingStudent = await _context.Students
                .Include(requesting => requesting.SurveyStudent)
                .FirstOrDefaultAsync(requesting => requesting.UserId == studentId)
                ?? throw new ArgumentException($"There is not Studdent with Id {studentId}");

            var potentialRoommates = await _context.Students
                .Include(potential => potential.SurveyStudent)
                .Include(potential => potential.Interests)
                .Include(potential => potential.ProfilePicture)
                .Where(potential =>

                    potential.UserId != studentId &&

                    (potential.SurveyStudent.Party >= (requestingStudent.SurveyStudent.Party-2)) && 
                    (potential.SurveyStudent.Party <= (requestingStudent.SurveyStudent.Party + 2)) &&
                    potential.SurveyStudent.Smoking == requestingStudent.SurveyStudent.Smoking &&
                    potential.SurveyStudent.Sociability == requestingStudent.SurveyStudent.Sociability &&
                    potential.SurveyStudent.Animals == requestingStudent.SurveyStudent.Animals &&
                    potential.SurveyStudent.Vegan == requestingStudent.SurveyStudent.Vegan &&
                    potential.SurveyStudent.LookingForRoommate == requestingStudent.SurveyStudent.LookingForRoommate &&
                    potential.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates &&
                    potential.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender &&
                    (requestingStudent.SurveyStudent.MinRoommateAge <= (DateTime.Now.Year - potential.BirthDate.Year)) &&
                    (requestingStudent.SurveyStudent.MaxRoommateAge >= (DateTime.Now.Year - potential.BirthDate.Year)) &&
                    potential.SurveyStudent.City == requestingStudent.SurveyStudent.City &&
                    !_context.Matcher.Any(matcher =>
                        (matcher.Student1Id == studentId && matcher.Student2Id == potential.UserId && matcher.IsStudent1Interested != null) ||
                        (matcher.Student1Id == potential.UserId && matcher.Student2Id == studentId && matcher.IsStudent2Interested != null)
                     ))
                .Select(potential => new
                {
                    PotentialStudent = potential,
                    ConditionsMet = (
                        (potential.SurveyStudent.Party >= (requestingStudent.SurveyStudent.Party - 2)) &&
                        (potential.SurveyStudent.Party <= (requestingStudent.SurveyStudent.Party + 2)) ? 1 : 0) +
                        (potential.SurveyStudent.Smoking == requestingStudent.SurveyStudent.Smoking ? 1 : 0) +
                        (potential.SurveyStudent.Sociability == requestingStudent.SurveyStudent.Sociability ? 1 : 0) +
                        (potential.SurveyStudent.Animals == requestingStudent.SurveyStudent.Animals ? 1 : 0) +
                        (potential.SurveyStudent.Vegan == requestingStudent.SurveyStudent.Vegan ? 1 : 0) +
                        (potential.SurveyStudent.LookingForRoommate == requestingStudent.SurveyStudent.LookingForRoommate ? 1 : 0) +
                        (potential.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates ? 1 : 0) +
                        (potential.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender ? 1 : 0) +
                        ((requestingStudent.SurveyStudent.MinRoommateAge <= (DateTime.Now.Year - potential.BirthDate.Year)) ? 1 : 0) +
                        ((requestingStudent.SurveyStudent.MaxRoommateAge >= (DateTime.Now.Year - potential.BirthDate.Year)) ? 1 : 0) +
                        (potential.SurveyStudent.City == requestingStudent.SurveyStudent.City ? 1 : 0)
                })
                .Where(result => result.ConditionsMet >= Matcher.AgreementPercentage * Matcher.ValuesAmount) 
                .OrderByDescending(result => result.ConditionsMet)
                .Select(potential => _mapper.Map<StudentForMatcherDto>(potential.PotentialStudent))
                .ToListAsync();

            return potentialRoommates;
        }

        public async Task AcceptStudentAsync(int student1Id, int student2Id, bool isAccept)
        {
            var match = await _context.Matcher
                .Where(x => (x.Student1Id == student1Id && x.Student2Id == student2Id) || 
                            (x.Student1Id == student2Id && x.Student2Id == student1Id))
                .FirstOrDefaultAsync();

            if (match == null)
            {
                match = new Matcher
                {
                    Student1Id = Math.Min(student1Id, student2Id),
                    Student2Id = Math.Max(student1Id, student2Id)
                };
                await _context.Matcher.AddAsync(match);
            }
            
            if(student1Id==match.Student1Id)
            {
                match.IsStudent1Interested = isAccept;
            }
            else
            {
                match.IsStudent2Interested = isAccept;
            }

            if (match.IsStudent1Interested == true && match.IsStudent2Interested == true)
            {
                var baseUrl = _configuration.GetSection("AppBaseUrl").Value;
                var link = $"{baseUrl}/find-roommate";
                var emailBody = new StringBuilder();

                emailBody.AppendLine(EmailHelper.HtmlHTag($"Ktoś wyraża chęć zamieszkania z tobą!", 1))
                            .AppendLine(EmailHelper.HtmlPTag($"Aby zobaczyć kto to i rozpocząć czat naciśnij {EmailHelper.AddLinkToText(link, "TUTAJ")} lub przejdź pod poniższy link"))
                            .AppendLine(EmailHelper.HtmlPTag($"{link}"));

                await _notificationService.SendNotificationAsync(EmailTitles.NewMessage, emailBody.ToString(), TranslateKeys.NewMatchTitle, TranslateKeys.NewMatchBody, student1Id, false);
                await _notificationService.SendNotificationAsync(EmailTitles.NewMessage, emailBody.ToString(), TranslateKeys.NewMatchTitle, TranslateKeys.NewMatchBody, student2Id, false);
            }

            await _context.SaveChangesAsync();
        }
    }
}
