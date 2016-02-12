using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boxing.Contracts;
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
            User user = await _context.Users.Include(u => u.Predictions.Select(p => p.Match)).FirstOrDefaultAsync(u => u.Id == userId);
            if (user ==  null)
            {
                throw new NotFoundException();
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Rating = user.Predictions.Count(p => p.Match.Winner == p.PredictedWinner && p.Match.StatusId == (int)MatchStatusesEnum.Finished) * 100D / user.Predictions.Count,
            };
            return userDto;
        }

        public async Task<UserDto> GetUserAsync(string username, string password)
        {
            User user = await _context.Users.Include(u => u.Predictions.Select(p => p.Match)).FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new NotFoundException();
            }
         
            byte[] submittedPasswordHash = PasswordHash.GenerateSaltedHash(password, user.PasswordSalt);
            var isValidPassword = user.PasswordHash.SequenceEqual(submittedPasswordHash);
            if (!isValidPassword)
            {
                throw new NotFoundException();
            }
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Rating = user.Predictions.Count(p => p.Match.Winner == p.PredictedWinner && p.Match.StatusId == (int)MatchStatusesEnum.Finished) * 100D / user.Predictions.Count,
            };
            return userDto;
        }


        public async Task<IEnumerable<UserDto>> GetUsersAsync(int skip, int take, string sortBy, string order)
        {
            if (skip < 0)
            {
                throw new BadRequestException("The skip parameter must be a non-negative value");
            }

            if (take < 0)
            {
                throw new BadRequestException("The take parameter must be a non-negative value");
            }

            if (sortBy != "rating" && sortBy != "fullName")
            {
                throw new BadRequestException("Invalid sortBy parameter");
            }

            if (order != "asc" && order != "desc")
            {
                throw new BadRequestException("Inalid order parameter");
            }

            IQueryable<User> query = _context.Users;
            if (sortBy == "rating")
            {
                if (order == "asc")
                {
                    query = query.OrderBy(u => u.Rating);
                }
                else if (order == "desc")
                {
                    query = query.OrderByDescending(u => u.Predictions.Count(p => p.Match.Winner == p.PredictedWinner) * 100D / u.Predictions.Count);
                }
            }
            else if (sortBy == "fullName")
            {

                if (order == "asc")
                {
                    query = query.OrderBy(u => u.FullName);
                }
                else if (order == "desc")
                {
                    query = query.OrderByDescending(u => u.FullName);
                }
            }
            IEnumerable<UserDto> users = await query.Skip(skip).Take(take).Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Username = u.Username,
                Rating = u.Predictions.Count(p => p.Match.Winner == p.PredictedWinner && p.Match.StatusId == (int)MatchStatusesEnum.Finished) * 100D / u.Predictions.Count,
            }).ToListAsync();
            return users;
        }

        public async Task<UserDto> AddUserAsync(UserDto user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                throw new BadRequestException($"User with username {user.Username} already exists");
            }

            byte[] salt = PasswordHash.GenerateSalt();
            byte[] saltedPassword = PasswordHash.GenerateSaltedHash(user.Password, salt);

            var role = _context.Roles.FirstOrDefault(r => r.Id == (int)RolesEnum.User);

            var created = _context.Users.Add(new User
            {
                Username = user.Username,
                FullName = user.FullName,
                PasswordHash = saltedPassword,
                PasswordSalt = salt,
                AuthenticationToken = Guid.Empty,
                Role = role
            });
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Id = created.Id,
                Rating = created.Rating
            };
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null)
            {
                throw new NotFoundException();
            }

            user.FullName = userDto.FullName;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {userId} could not be found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<UserDto> GetUserAsync(string username)
        {
            var user = await _context.Users.Include(u => u.Predictions.Select(p => p.Match)).FirstOrDefaultAsync(u => u.Username == username);
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Rating = user.Predictions.Count(p => p.Match.Winner == p.PredictedWinner && p.Match.StatusId == (int)MatchStatusesEnum.Finished) * 100D / user.Predictions.Count,
                Username = user.Username
            };
        }
    }
}
