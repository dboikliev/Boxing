using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts;
using Boxing.Contracts.Dto;
using Boxing.Core.DataAccess;
using Boxing.Core.DataAccess.Entities;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Core.Services.Implementations
{
    public class PredictionsService : IPredictionsService
    {
        private readonly BoxingContext _context;

        public PredictionsService()
        {
            _context = new BoxingContext();;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<PredictionDto> GetPredictionAsync(int userId, int matchId)
        {
            var prediction = await _context.Predictions.FirstOrDefaultAsync(p => p.UserId == userId && p.MatchId == matchId);
            if (prediction == null)
            {
                throw new NotFoundException($"Prediction for user with Id {userId} and MatchId {matchId} could not be found.");
            }

            return new PredictionDto
            {
                Id = prediction.Id,
                MatchId = prediction.MatchId,
                PredictedWinner = prediction.PredictedWinner,
                UserId = prediction.UserId
            };
        }

        public async Task<PredictionDto> AddPredictionAsync(PredictionDto predictionDto)
        {
            if (_context.Matches.FirstOrDefault(m => m.Id == predictionDto.MatchId)?.StatusId != (int)MatchStatusesEnum.Active)
            {
                throw new BadRequestException("Predictions can be made only for active matches");
            }

            var prediction = _context.Predictions.Add(new Prediction
            {
                UserId = predictionDto.UserId,
                MatchId = predictionDto.MatchId,
                PredictedWinner = predictionDto.PredictedWinner
            });
            await _context.SaveChangesAsync();
            return new PredictionDto
            {
                Id = prediction.Id,
                UserId = prediction.UserId,
                MatchId = prediction.MatchId,
                PredictedWinner = prediction.PredictedWinner
            };
        }
    }
}
