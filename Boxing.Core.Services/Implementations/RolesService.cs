using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts;
using Boxing.Core.DataAccess;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Core.Services.Implementations
{
    public class RolesService: IRolesService
    {
        private readonly BoxingContext _context;

        public RolesService()
        {
            _context = new BoxingContext();
        }


        public async Task<RolesEnum> GetRoleForAuthenticationTokenAsync(string authenticationToken)
        {
            var user =
                (await _context.Users.FirstOrDefaultAsync(u => u.AuthenticationToken.ToString() == authenticationToken));
            if (user == null)
            {
                throw new NotFoundException($"User with authentication token {authenticationToken} does not exist.");
            }

            return (RolesEnum)user.RoleId;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
