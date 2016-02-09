using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;
using Boxing.Core.DataAccess;
using Boxing.Core.DataAccess.Entities;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Helpers;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Core.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly BoxingContext _context;

        public UsersService()
        {
            _context = new BoxingContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user ==  null)
            {
                throw new NotFoundException();
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName
            };
            return userDto;
        }

        public async Task<UserDto> GetUserAsync(string username, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new NotFoundException();
            }
         
            byte[] submittedPasswordHash = PasswordHash.GenerateSaltedHash(password, user.PasswordSalt);
            var isValidPassword = Enumerable.SequenceEqual(user.PasswordHash, submittedPasswordHash);
            if (!isValidPassword)
            {
                throw new NotFoundException();
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName
            };
            return userDto;
        }


        public async Task<IEnumerable<UserDto>> GetUsersAsync(int skip, int take, string sortBy, string order)
        {
            IQueryable<User> query = _context.Users;
            if (sortBy == "rating")
            {
                query = query.OrderBy(u => u.Rating);
            }
            else
            {
                query = query.OrderBy(u => u.FullName);
            }
            IEnumerable<UserDto> users = await query.Skip(skip).Take(take).Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName
            }).ToListAsync();
            return users;
        }

        public void CreateUser(UserDto user)
        {
            byte[] salt = PasswordHash.GenerateSalt();
            byte[] saltedPassword = PasswordHash.GenerateSaltedHash(user.Password, salt);

            _context.Users.Add(new User
            {
                FullName = user.FullName,
                PasswordHash = saltedPassword,
                PasswordSalt = salt
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

            user.FullName = userDto.FullName;
        }
    }
}
