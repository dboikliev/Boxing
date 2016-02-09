using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Http;
using Boxing.Api.Services.Models;
using Boxing.Contracts.Dto;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Controllers
{
    public class LoginsController : ApiController
    {
        private readonly ILoginsService _loginsService;
        private readonly IUsersService _usersService;

        public LoginsController(ILoginsService loginsService, IUsersService usersService)
        {
            _loginsService = loginsService;
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(LoginModel login)
        {
            UserDto userDto = await _usersService.GetUserAsync(login.Username, login.Password);
            LoginDto loginDto = _loginsService.CreateLogin(userDto.Id);
            await _loginsService.SaveAsync();
            var result = new LoginModel()
            {
                Id = loginDto.Id,
                AuthenticationToken = login.AuthenticationToken
            };
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }


        [HttpDelete]
        public async Task Delete(int id)
        {
            await _loginsService.DeleteLoginAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _loginsService.Dispose();
        }
    }
}
