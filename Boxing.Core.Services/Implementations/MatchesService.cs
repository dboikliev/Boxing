using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts;
using Boxing.Contracts.Dto;
using Boxing.Core.DataAccess;
using Boxing.Core.DataAccess.Entities;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Core.Services.Implementations
{
    public class MatchesService : IMatchesService
    {
        private readonly BoxingContext _context;

        public MatchesService()
        {
            _context = new BoxingContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<MatchDto> AddMatchAsync(MatchDto match)
        {
            var addedMatch = _context.Matches.Add(new Match
            {
                Boxer1 = match.Boxer1,
                Boxer2 = match.Boxer2,
                DateOfMatch = match.DateOfMatch,
                Description = match.Description,
                Place = match.Place,
                StatusId = (int)MatchStatusesEnum.Active
            });
            await _context.SaveChangesAsync();
            return new MatchDto
            {
                Id = addedMatch.Id,
                Boxer1 = addedMatch.Boxer1,
                Boxer2 = addedMatch.Boxer2,
                DateOfMatch = addedMatch.DateOfMatch,
                Description = addedMatch.Description,
                Place = addedMatch.Place,
                Status = (MatchStatusesEnum) addedMatch.StatusId
            };
        }

        public async Task<MatchDto> GetMatchAsync(int id)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                throw new NotFoundException();
            }
            var matchDto = new MatchDto
            {
                Id = match.Id,
                Boxer1 = match.Boxer1,
                Boxer2 = match.Boxer2,
                DateOfMatch = match.DateOfMatch,
                Description = match.Description,
                Place = match.Place,
                Winner = match.Winner,
                Status = (MatchStatusesEnum)match.StatusId
            };
            return matchDto;
        }

        public async Task<IEnumerable<MatchDto>> GetMatches(int skip, int take)
        {
            var matches = await _context.Matches.OrderBy(m => m.Id).Skip(skip).Take(take).Select(match => new MatchDto
            {
                Id = match.Id,
                Boxer1 = match.Boxer1,
                Boxer2 = match.Boxer2,
                DateOfMatch = match.DateOfMatch,
                Description = match.Description,
                Place = match.Place,
                Winner = match.Winner,
                Status = (MatchStatusesEnum)match.StatusId
            }).ToListAsync();
            return matches;
        }

        public Task<int> GetMatchesCountAsync()
        {
            return _context.Matches.CountAsync();
        }

        public async Task UpdateMatchAsync(MatchDto matchDto)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == matchDto.Id);
            match.StatusId = (int) matchDto.Status;
            match.Boxer2 = matchDto.Boxer2;
            match.Boxer1 = matchDto.Boxer1;
            match.Place = matchDto.Place;
            match.DateOfMatch = matchDto.DateOfMatch;
            match.Description = matchDto.Description;
            match.Winner = matchDto.Winner;
            await _context.SaveChangesAsync();
        }

        public async Task SetStatusAsync(int id, MatchStatusesEnum status)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                throw new NotFoundException($"Could not find match with id {id}");
            }
            match.StatusId = (int) status;
            await _context.SaveChangesAsync();
        }
    }
}
