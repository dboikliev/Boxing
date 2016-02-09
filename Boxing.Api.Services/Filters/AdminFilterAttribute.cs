using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Filters
{
    public class AdminFilterAttribute : AuthorizationFilterAttribute
    {
        public override async void OnAuthorization(HttpActionContext actionContext)
        {
            using (var scope = actionContext.Request.GetDependencyScope())
            {
                var header = actionContext.Request.Headers.FirstOrDefault(h => h.Key == "Admin-Token");
                var tokenValue = header.Value?.FirstOrDefault();
                if (string.IsNullOrEmpty(tokenValue))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    "Missing authentication token");
                    return;
                }

                if (tokenValue != "secure_admin_token")
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                        "Invalid authentication token");
                    return;
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}