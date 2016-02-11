using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;

namespace Boxing.Core.Services.Interfaces
{
    public interface ILoginsService : IDisposable
    {
        Task<bool> IsValidTokenAsync(string authenticationToken);
        Task<LoginDto> CreateLoginAsync(int userId);
        Task DeleteLoginAsync(int userId);
        Task SaveAsync();
    }
}
