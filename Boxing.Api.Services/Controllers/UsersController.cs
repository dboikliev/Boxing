using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Boxing.Api.Services.Models;
using Boxing.Contracts.Dto;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<UserDto> Get(string username)
        {
            return await _usersService.GetUserAsync(username);
        }

        [HttpGet]
        public async Task<UsersListModel> Get(int skip, int take, string sortBy, string order)
        {
            var users = await _usersService.GetUsersAsync(skip, take, sortBy, order);
            return new UsersListModel
            {
                Skipped = skip,
                Total = await _usersService.GetUsersCountAsync(),
                Users = users.Select(u => new UserModel
                {
                    UserId = u.Id,
                    FullName = u.FullName,
                    Rating = u.Rating
                })
            };
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create([FromBody]UserModel user)
        {
            var userDto = new UserDto
            {
                Username = user.Username,
                FullName = user.FullName,
                Password = user.Password
            };
            var created = await _usersService.AddUserAsync(userDto);
            return Request.CreateResponse(HttpStatusCode.Created, new UserModel
            {
                UserId = created.Id,
                Rating = created.Rating
            });
        }

        [HttpPut]
        public async Task Update(int id, [FromBody] UserModel user)
        {
            var userDto = new UserDto
            {
                Id = id,
                FullName = user.FullName
            };

            await _usersService.UpdateUserAsync(userDto);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            await _usersService.DeleteUserAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _usersService.Dispose();
        }
    }
}
