using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestTestWebApp.Models;

namespace RestTestWebApp.Services
{
    public interface IWebClientService
    {
        ApiResponse<TResponse> ExecuteGet<TResponse>(ApiRequest request) where TResponse : new();

        ApiResponse<TResponse> ExecutePost<TResponse>(ApiRequest request) where TResponse : new();

        ApiResponse<TResponse> ExecutePut<TResponse>(ApiRequest request) where TResponse : new();

        void ExecuteDelete(ApiRequest request);
    }
}