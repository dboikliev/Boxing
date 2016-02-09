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
        LoginDto CreateLogin(int userId);
        Task DeleteLoginAsync(int loginId);
        Task SaveAsync();
    }
}
