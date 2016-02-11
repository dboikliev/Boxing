using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Boxing.Contracts;
using Boxing.Contracts.Dto;
using Boxing.Core.DataAccess;
using Boxing.Core.DataAccess.Entities;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Core.Services.Implementations
{
    public class LoginsService : ILoginsService
    {
        private readonly BoxingContext _context;

        public LoginsService()
        {
            _context = new BoxingContext();
        }

        public async Task<bool> IsValidTokenAsync(string authenticationToken)
        {
            return await _context.Users.AnyAsync(user => user.AuthenticationToken.ToString() == authenticationToken);
        }

        public async Task<LoginDto> CreateLoginAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.AuthenticationToken = Guid.NewGuid();
            }
            else
            {
                throw new NotFoundException("User not found.");
            }

            return new LoginDto
            {
                Id = user.Id,
                AuthenticationToken = user.AuthenticationToken.ToString(),
                Username = user.Username,
                Role = (RolesEnum)user.RoleId
            };
        }

        public async Task DeleteLoginAsync(int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }
            user.AuthenticationToken = Guid.Empty;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
