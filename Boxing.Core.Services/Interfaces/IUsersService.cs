using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;

namespace Boxing.Core.Services.Interfaces
{
    public interface IUsersService : IDisposable
    {
        Task<UserDto> GetUserAsync(int userId);
        Task<bool> IsValidToken(string tokenValue);
        Task<IEnumerable<UserDto>> GetUsersAsync(int skip, int take);
        void CreateUser(UserDto user);
        Task SaveAsync();
        Task UpdateUser(UserDto user);
    }
}
