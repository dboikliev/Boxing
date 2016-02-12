using System.Net;
using System.Web.Mvc;
using Boxing.Web.Constants;
using Boxing.Web.Models;
using Boxing.Web.ViewModels;
using RestTestWebApp.Models;
using RestTestWebApp.Services;

namespace Boxing.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IWebClientService _webClientService;

        public UsersController(IWebClientService webClientService)
        {
            _webClientService = webClientService;
        }

        public ActionResult Manage(int skip = 0, int take = PagingConstants.UsersPerPage, string sortBy = "fullName", string order = "asc" )
        {
            var response = _webClientService.ExecuteGet<GetUsersListResponse>(new ApiRequest
            {
                EndPoint = $"users?skip={skip}&take={take}&sortBy={sortBy}&order={order}"
            });

            var matchesListModel = new ListViewModel<UserViewModel>
            {
                Items = response.Payload.Users,
                Total = response.Payload.Total,
                Skipped = response.Payload.Skipped,
                SortedBy = sortBy,
                IsAscending = order == "asc"
            };
            return View("Manage", matchesListModel);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            var request = new ApiRequest
            {
                EndPoint = $"delete/{id}",
            };
            request.Headers["Authentication-Token"] = Session["Authentication-Token"] as string;
            _webClientService.ExecuteDelete(request);
            return RedirectToAction("Manage");
        }

        public ActionResult UserDetails(string username)
        {
            var response = _webClientService.ExecuteGet<UserViewModel>(new ApiRequest
            {
                EndPoint = $"users?username={username}"
            });

            return View(response.Payload);
        }
    }
}