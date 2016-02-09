using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Boxing.Web.Models;
using Microsoft.Owin.Security.Google;
using RestTestWebApp.Models;
using RestTestWebApp.Services;

namespace Boxing.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebClientService _webClientService;
        public AccountController(IWebClientService webClientService)
        {
            _webClientService = webClientService;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var response =
                _webClientService.ExecutePost<LoginViewModel>(new ApiRequest()
                {
                    EndPoint = "logins",
                    Request = model
                });
            if (response != null)
            {
                Session["Username"] = response.Username;
                return View(response);
            }
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View();
        }

        public async Task<ActionResult> LogOut()
        {
            return View();
        }

        public async Task<ActionResult> Register()
        {
            return View();
        }
    }
}