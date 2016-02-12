using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boxing.Web.Constants;
using Boxing.Web.Models;
using Boxing.Web.ViewModels;
using RestTestWebApp.Models;
using RestTestWebApp.Services;

namespace Boxing.Web.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IWebClientService _webClientService;
        public MatchesController(IWebClientService webClientService)
        {
            _webClientService = webClientService;
        }

        // GET: Match
        public ActionResult Index(int skip = 0 , int take = 5)
        {
            var response = _webClientService.ExecuteGet<GetMatchesListResponse>(new ApiRequest
            {
                EndPoint = $"matches?skip={skip}&take={take}"
            });

            var matchesListModel = new ListViewModel<MatchViewModel>
            {
                Items = response.Payload.Matches,
                Total = response.Payload.Total,
                Skipped = response.Payload.Skipped
            };

            return View(matchesListModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MatchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var request = new ApiRequest
            {
                EndPoint = "matches",
                Request = model
            };
            request.Headers["Authentication-Token"] = Session["Authentication-Token"] as string;
            _webClientService.ExecutePost<CreateMatchResponse>(request);
            return Manage();
        }

        public ActionResult CreatePrediction(int matchId)
        {
            return View(new PredictionViewModel() { MatchId = matchId });
        }

        [HttpPost]
        public ActionResult CreatePrediction(PredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.UserId = ((int?) Session["UserId"]).GetValueOrDefault();
            var request = new ApiRequest
            {
                EndPoint = $"matches/{model.MatchId}/predictions",
                Request = model
            };
            request.Headers["Authentication-Token"] = Session["Authentication-Token"] as string;
            _webClientService.ExecutePost<object>(request);
            return View("Index");
        }

        public ActionResult Edit(int matchId)
        {
            var response = _webClientService.ExecuteGet<MatchViewModel>(new ApiRequest
            {
                EndPoint = $"matches/{matchId}"
            });
            return View(response.Payload);
        }

        [HttpPost]
        public ActionResult Edit(MatchViewModel model)
        {
            if (model.Winner.HasValue)
            {
                model.Status = MatchStatusesEnum.Finished;   
            }

            var request = new ApiRequest()
            {
                EndPoint = $"matches?id={model.Id}",
                Request = model
            };
            request.Headers["Authentication-Token"] = Session["Authentication-Token"] as string;
            var response = _webClientService.ExecutePut<ApiResponse<object>>(request);
            return Manage();
        }

        public ActionResult Manage(int skip = 0, int take = PagingConstants.MatchesPerPage)
        {
            var response = _webClientService.ExecuteGet<GetMatchesListResponse>(new ApiRequest
            {
                EndPoint = $"matches?skip={skip}&take={take}"
            });

            var matchesListModel = new ListViewModel<MatchViewModel>()
            {
                Items = response.Payload.Matches,
                Total = response.Payload.Total,
                Skipped = response.Payload.Skipped
            };
            return View("Manage", matchesListModel);
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var request = new ApiRequest
            {
                EndPoint = $"matches/{id}/status",
                Request = (int)MatchStatusesEnum.Canceled
                
            };
            request.Headers["Authentication-Token"] = Session["Authentication-Token"] as string;
            _webClientService.ExecutePut<object>(request);
            return Manage();
        }
    }
}