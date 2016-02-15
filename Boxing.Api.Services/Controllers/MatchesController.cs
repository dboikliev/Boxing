using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Validation;
using Boxing.Api.Services.Filters;
using Boxing.Api.Services.Models;
using Boxing.Contracts;
using Boxing.Contracts.Dto;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly IMatchesService _matchesService;
        private readonly IPredictionsService _predictionsService;

        public MatchesController(IMatchesService matchesService, IPredictionsService predictionsService)
        {
            _matchesService = matchesService;
            _predictionsService = predictionsService;
        }

        [HttpGet]
        public async Task<MatchModel> Get(int id)
        {
            MatchDto matchDto = await _matchesService.GetMatchAsync(id);
            return new MatchModel
            {
                Id = matchDto.Id,
                Boxer1 = matchDto.Boxer1,
                Boxer2 = matchDto.Boxer2,
                DateOfMatch = matchDto.DateOfMatch,
                Description = matchDto.Description,
                Place = matchDto.Place,
                Winner = matchDto.Winner,
                Status = matchDto.Status
            };
        }

        [HttpGet]
        public async Task<MatchesListModel> Get(int skip, int take)
        {
                var matches = await _matchesService.GetMatches(skip, take);
            var matchesListModel = new MatchesListModel
            {
                Total = await _matchesService.GetMatchesCountAsync(),
                Skipped = skip,
                Matches = matches.Select(m => new MatchModel
                {
                    Id = m.Id,
                    Boxer1 = m.Boxer1,
                    Boxer2 = m.Boxer2,
                    DateOfMatch = m.DateOfMatch,
                    Description = m.Description,
                    Place = m.Place,
                    Winner = m.Winner,
                    Status = m.Status
                })
            };
            return matchesListModel;
        }

        [HttpPost]
        [ActionName("predictions")]
        [AuthorizationTokenFilter]
        public async Task<HttpResponseMessage> CreatePrediction(int matchId, [FromBody]PredictionModel model)
        {
             var predictionDto = await _predictionsService.AddPredictionAsync(new PredictionDto
             {
                 MatchId = matchId,
                 PredictedWinner = model.PredictedWinner,
                 UserId = model.UserId
             });
            return Request.CreateResponse(HttpStatusCode.Created, new PredictionModel
            {
                Id = predictionDto.Id,
                MatchId = predictionDto.MatchId,
                UserId = predictionDto.UserId,
                PredictedWinner = predictionDto.PredictedWinner
            });
        }

        [HttpGet]
        [ActionName("predictions")]
        [AuthorizationTokenFilter]
        public async Task<HttpResponseMessage> GetPrediction(int matchId, [FromBody]int userId)
        {
            var predictionDto = await _predictionsService.GetPredictionAsync(userId, matchId);
            return Request.CreateResponse(HttpStatusCode.Created, new PredictionModel
            {
                Id = predictionDto.Id,
                MatchId = predictionDto.MatchId,
                UserId = predictionDto.UserId,
                PredictedWinner = predictionDto.PredictedWinner
            });
        }

        [HttpPut]
        [ActionName("update")]
        [AdminFilter]
        public async Task Update(int id, [FromBody] MatchModel model)
        {
            MatchDto matchDto = await _matchesService.GetMatchAsync(id);
            matchDto.Winner = model.Winner;
            matchDto.Boxer1 = model.Boxer1;
            matchDto.Boxer2 = model.Boxer2;
            matchDto.DateOfMatch = model.DateOfMatch;
            matchDto.Description = model.Description;
            matchDto.Place = model.Place;
            if (model.Winner.HasValue)
            {
                matchDto.Status = MatchStatusesEnum.Finished;
            }
            await _matchesService.UpdateMatchAsync(matchDto);
        }

        [AdminFilter]
        [HttpPost]
        public async Task<HttpResponseMessage> Create([FromBody] MatchModel match)
        {
            var matchDto = new MatchDto
            {
                Boxer1 = match.Boxer1,
                Boxer2 = match.Boxer2,
                DateOfMatch = match.DateOfMatch,
                Description = match.Description,
                Place = match.Place
            };
            await _matchesService.AddMatchAsync(matchDto);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [AdminFilter]
        [ActionName("status")]
        [HttpPut]
        public async Task SetStatus(int matchId, [FromBody]int status)
        {
            await _matchesService.SetStatusAsync(matchId, (MatchStatusesEnum)status);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _matchesService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}