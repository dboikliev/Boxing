using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxing.Contracts.Dto;

namespace Boxing.Core.Services.Interfaces
{
    public interface IPredictionsService : IDisposable
    {
        Task<PredictionDto> GetPredictionAsync(int userId, int matchId);
        Task<PredictionDto> AddPredictionAsync(PredictionDto predictionDto);
    }
}
