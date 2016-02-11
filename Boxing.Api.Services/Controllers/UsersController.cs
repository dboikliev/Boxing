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
        public async Task<UserDto> Get(int id)
        {
            return await _usersService.GetUserAsync(id);
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
            _usersService.CreateUser(userDto);
            await _usersService.SaveAsync();
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task Update(int id, [FromBody] UserModel user)
        {
            var userDto = new UserDto
            {
                Id = id,
                FullName = user.FullName
            };

            var update = _usersService.UpdateUser(userDto);
            var save = _usersService.SaveAsync();
            await Task.WhenAll(update, save);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _usersService.Dispose();
        }
    }
}
