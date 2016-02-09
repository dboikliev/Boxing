using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;

namespace Boxing.Core.Services.Interfaces
{
    public interface IMatchesService : IDisposable
    {
        void CreateMatch(MatchDto match);
        Task<MatchDto> GetMatchAsync(int id);
        Task<IEnumerable<MatchDto>> GetMatches(int skip, int take);
        Task UpdateMatchAsync(MatchDto match);
        Task SaveAsync();
    }
}
