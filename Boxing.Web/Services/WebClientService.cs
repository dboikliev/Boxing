using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RestTestWebApp.Models;

namespace RestTestWebApp.Services
{
    public class WebClientService : IWebClientService
    {
        IConfigurationService configurationService;

        public WebClientService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ApiResponse<TResponse> ExecuteGet<TResponse>(ApiRequest request) where TResponse : new()
        {
            try
            {
                using (HttpClient client = GetHttpClient())
                {
                    foreach (var header in request.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    using (var response = client.GetAsync(request.EndPoint).GetAwaiter().GetResult())
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var payload =  JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                            return new ApiResponse<TResponse>() {Payload = payload, HttpStatusCode =  response.StatusCode };
                        }

                        return new ApiResponse<TResponse>() { Payload = default(TResponse), HttpStatusCode = response.StatusCode };
                    }
                }
            }
            catch
            {
                return new ApiResponse<TResponse>() { Payload = default(TResponse) };
            }
        }

        public ApiResponse<TResponse> ExecutePost<TResponse>(ApiRequest request) where TResponse : new()
        {
            try
            {
                using (HttpClient client = GetHttpClient())
                {
                    using (StringContent requestContent = new StringContent(request.Request != null ? JsonConvert.SerializeObject(request.Request) : string.Empty, Encoding.UTF8, "application/json"))
                    {
                        foreach (var header in request.Headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                        using (var response = client.PostAsync(request.EndPoint, requestContent).GetAwaiter().GetResult())
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var payload = JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                                return new ApiResponse<TResponse>() { Payload = payload, HttpStatusCode = response.StatusCode };
                            }

                            return new ApiResponse<TResponse>() { Payload = default(TResponse), HttpStatusCode = response.StatusCode };
                        }
                    }
                }
            }
            catch
            {
                return new ApiResponse<TResponse>() { Payload = default(TResponse) };
            }
        }

        public ApiResponse<TResponse> ExecutePut<TResponse>(ApiRequest request) where TResponse : new()
        {
            try
            {
                using (HttpClient client = GetHttpClient())
                {
                    foreach (var header in request.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    using (StringContent requestContent = new StringContent(request.Request != null ? JsonConvert.SerializeObject(request.Request) : string.Empty, Encoding.UTF8, "application/json"))
                    {
                        using (var response = client.PutAsync(request.EndPoint, requestContent).GetAwaiter().GetResult())
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var payload = JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                                return new ApiResponse<TResponse>() { Payload = payload, HttpStatusCode = response.StatusCode };
                            }

                            return new ApiResponse<TResponse>() { Payload = default(TResponse), HttpStatusCode = response.StatusCode };
                        }
                    }
                }
            }
            catch
            {
                return new ApiResponse<TResponse>() { Payload = default(TResponse) };
            }
        }

        public void ExecuteDelete(ApiRequest request)
        {
            try
            {
                using (HttpClient client = GetHttpClient())
                {
                    foreach (var header in request.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    using (var response = client.DeleteAsync(request.EndPoint).GetAwaiter().GetResult())
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip;
            }

            HttpClient client = new HttpClient(handler);

            client.BaseAddress = new Uri(configurationService.GetValue("ApiBaseUrl"));

            client.Timeout = TimeSpan.FromSeconds(20);

            if (handler.SupportsAutomaticDecompression)
            {
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                client.DefaultRequestHeaders.Add("Auth-Token", "thetoken");
            }

            return client;
        }
    }
}