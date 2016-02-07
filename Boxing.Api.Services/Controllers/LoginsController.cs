using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _usersService.Dispose();
        }
    }
}
