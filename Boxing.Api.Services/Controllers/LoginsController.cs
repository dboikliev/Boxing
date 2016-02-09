using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.DynamicData;
using System.Web.Http;
using Boxing.Api.Services.Models;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Controllers
{
    public class LoginsController : ApiController
    {
        private readonly IUsersService _usersService;

        public LoginsController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create(LoginModel login)
        {
            throw new NotImplementedException();;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _usersService.Dispose();
        }
    }
}
