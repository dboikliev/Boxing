using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;

namespace Boxing.Core.Services.Interfaces
{
    public interface IUsersService : IDisposable
    {
        Task<UserDto> GetUserAsync(int userId);
        Task<UserDto> GetUserAsync(string username, string password);
        Task<IEnumerable<UserDto>> GetUsersAsync(int skip, int take, string sortBy, string order);
        void CreateUser(UserDto user);
        Task SaveAsync();
        Task UpdateUser(UserDto user);
    }
}
