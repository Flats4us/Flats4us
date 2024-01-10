using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Flats4us.Services
{
    public class TechnicalProblemService : ITechnicalProblemService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public TechnicalProblemService(Flats4usContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountedListDto<TechnicalProblemDto>> GetAllAsync(PaginatorDto input)
        {
            var problems = await _context.TechnicalProblems
                    .OrderBy(x => x.Solved)  
                    .ThenBy(x => x.Date)
                    .Select(e => _mapper.Map<TechnicalProblemDto>(e))
                    .ToListAsync();

            problems = problems
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            var result = new CountedListDto<TechnicalProblemDto>
            {
                TotalCount = problems.Count,
                Result = problems
            };

            return result;
        }

        public async Task PostAsync(AddTechnicalProblemDto input, int id)
        {
            var problem = new TechnicalProblem
            {
                Kind = input.Kind,
                Description = input.Description,
                Date = DateTime.Now,
                Solved = false,
                UserId = id
            };

            await _context.TechnicalProblems.AddAsync(problem);
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(int id)
        {
            var problem = await _context.TechnicalProblems.FindAsync(id);
            problem.Solved = true;

            await _context.SaveChangesAsync();
        }
    }
}
