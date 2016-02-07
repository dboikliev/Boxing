using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;
using Boxing.Core.DataAccess;
using Boxing.Core.DataAccess.Entities;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Core.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly BoxingContext _context;

        public UsersService(BoxingContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return userDto;
        }
        
        public async Task<IEnumerable<UserDto>> GetUsersAsync(int skip, int take)
        {
            IEnumerable<UserDto> users = await _context.Users.OrderBy(u => u.Id).Skip(skip).Take(take).Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).ToListAsync();
            return users;
        }

        public void CreateUser(string firstName, string lastName)
        {
            _context.Users.Add(new User
            {
                FirstName = firstName,
                LastName = lastName
            });
        }

        public void CreateUser(UserDto user)
        {
            _context.Users.Add(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null)
            {
                throw new NotFoundException();
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
        }
    }
}
