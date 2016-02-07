using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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
        public async Task<UserDto> Get(int userId)
        {
            return await _usersService.GetUserAsync(userId);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(string firstName, string lastName, string password)
        {
            _usersService.CreateUser(firstName, lastName);
            await _usersService.SaveAsync();
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task Update(int id, [FromBody] UserDto user)
        {
            user.Id = id;
            var update = _usersService.UpdateUser(user);
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
