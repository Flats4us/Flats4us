﻿using AutoMapper;
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

        public async Task<CountedListDto<TechnicalProblemForMapperDto>> GetAllAsync(PaginatorDto input)
        {
            var problems= await _context.TechnicalProblems
                    .OrderBy(x => x.Solved)  
                    .ThenBy(x => x.Date)     
                    .ToListAsync();

            var problems2 = _mapper.Map<List<TechnicalProblemForMapperDto>>(problems);

            problems2 =problems2
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToList();

            var result = new CountedListDto<TechnicalProblemForMapperDto>
            {
                TotalCount = problems2.Count,
                Result = problems2
            };

            return result;
        }

        public async Task PostAsync(TechnicalProblemDto input)
        {
            var problem = new TechnicalProblem
            {
                Kind = input.Kind,
                Description = input.Description,
                Date = DateTime.Now,
                Solved = false,
                UserId = input.UserId
            };

            await _context.TechnicalProblems.AddAsync(problem);
            await _context.SaveChangesAsync();
        }

        public async Task PutAsync(int id)
        {
            var problem = await _context.TechnicalProblems.FindAsync(id);
            problem.Solved=true;

            await _context.SaveChangesAsync();
        }


        //public async Task Delete(int id)
        //{
        //    var problem = await _context.TechnicalProblems.FindAsync(id);

        //    _context.TechnicalProblems.Remove(problem);
        //    await _context.SaveChangesAsync();
        //}
    }
}
