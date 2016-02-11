using System;
using System.Threading.Tasks;
using Boxing.Contracts;

namespace Boxing.Core.Services.Interfaces
{
    public interface IRolesService : IDisposable
    {
        Task<RolesEnum> GetRoleForAuthenticationTokenAsync(string authenticationToken);
    }
}
