using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Filters
{
    public class AuthorizationTokenFilterAttribute : AuthorizationFilterAttribute
    {
        protected string AuthenticationToken { get; set; }

        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            using (var scope = actionContext.Request.GetDependencyScope())
            {
                using (ILoginsService usersService = (ILoginsService)scope.GetService(typeof(ILoginsService)))
                {
                    var header = actionContext.Request.Headers.FirstOrDefault(h => h.Key == "Authentication-Token");
                    var tokenValue = header.Value?.FirstOrDefault();
                    if (string.IsNullOrEmpty(tokenValue))
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                        "Missing authentication token");
                        return;
                    }

                    var isValidToken = await usersService.IsValidTokenAsync(tokenValue);
                    if (!isValidToken)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                            "Invalid authentication token");
                        return;
                    }
                    AuthenticationToken = tokenValue;
                }
            }
            await base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
    }
}