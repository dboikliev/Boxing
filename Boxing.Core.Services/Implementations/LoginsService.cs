using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            return await _context.Logins.AnyAsync(login => login.AuthorizationToken == authenticationToken);
        }

        public LoginDto CreateLogin(int userId)
        {
            var login = new Login
            {
                Id = userId,
                AuthorizationToken = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now.ToUniversalTime(),
            };
            var created = _context.Logins.Add(login);
            return new LoginDto
            {
                Id = created.Id,
                AuthenticationToken = created.AuthorizationToken
            };
        }

        public async Task DeleteLoginAsync(int loginId)
        {
            Login loginEntity = await _context.Logins.FirstOrDefaultAsync(login => login.Id == loginId);
            if (loginEntity == null)
            {
                throw new NotFoundException();
            }
            _context.Logins.Remove(loginEntity);
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
