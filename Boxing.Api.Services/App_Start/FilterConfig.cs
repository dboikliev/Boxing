using System.Web.Http;
using Boxing.Api.Services.Filters;

namespace Boxing.Api.Services
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Filters.Add(new RequireSecureConnectionFilterAttribute());
            config.Filters.Add(new ExceptionToHttpStatusCodeFilterAttribute());
            config.Filters.Add(new ValidationFilterAttribute());
        }
    }
}