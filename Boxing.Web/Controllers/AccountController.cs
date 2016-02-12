using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Boxing.Web.Models;
using Boxing.Web.ViewModels;
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
            if (!ModelState.IsValid)    
            {
                return View();
            }
            var response =
                _webClientService.ExecutePost<LoginResponse>(new ApiRequest()
                {
                    EndPoint = "logins",
                    Request = model
                });
            if (response != null && response.Payload != null)
            {
                Session["Authentication-Token"] = response.Payload.AuthenticationToken;
                Session["UserId"] = response.Payload.Id;
                Session["Username"] = response.Payload.Username;
                Session["Role"] = response.Payload.Role;
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View();
        }

        public async Task<ActionResult> LogOff()
        {
            _webClientService.ExecuteDelete(new ApiRequest
            {
                EndPoint = $"logins/{(int?)Session["UserId"]}",
            });

            Session["Authentication-Token"] = null;
            Session["Id"] = null;
            Session["Username"] = null;
            Session["Role"] = null;
            return View("Login");
        }


        public async Task<ActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = _webClientService.ExecutePost<object>(new ApiRequest { EndPoint = "users", Request = model });
            if (response.HttpStatusCode != HttpStatusCode.Created)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");
                return View();
            }
            return View("Login");
        }
    }
}