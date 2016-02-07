using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Boxing.Api.Services.Filters;

namespace Boxing.Api.Services.App_Start
{
    public class FilterConfig
    {
        public static void RegisterFilters(HttpFilterCollection filterCollection)
        {
            filterCollection.Add(new RequireSecureConnectionFilterAttribute());
        }
    }
}