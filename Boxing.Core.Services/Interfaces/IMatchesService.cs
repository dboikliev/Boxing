using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts;
using Boxing.Contracts.Dto;

namespace Boxing.Core.Services.Interfaces
{
    public interface IMatchesService : IDisposable
    {
        Task<MatchDto> AddMatchAsync(MatchDto match);
        Task<MatchDto> GetMatchAsync(int id);
        Task<IEnumerable<MatchDto>> GetMatches(int skip, int take);
        Task UpdateMatchAsync(MatchDto match);
        Task<int> GetMatchesCountAsync();
        Task SetStatusAsync(int id, MatchStatusesEnum status);
    }
}
