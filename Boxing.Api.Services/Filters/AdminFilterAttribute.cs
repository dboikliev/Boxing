using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Boxing.Contracts;
using Boxing.Core.Services.Exceptions;
using Boxing.Core.Services.Interfaces;

namespace Boxing.Api.Services.Filters
{
    public class AdminFilterAttribute : AuthorizationTokenFilterAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await base.OnAuthorizationAsync(actionContext, cancellationToken);

            using (var scope = (IRolesService)actionContext.Request.GetDependencyScope().GetService(typeof(IRolesService)))
            {
                try
                {
                    var role = await scope.GetRoleForAuthenticationTokenAsync(base.AuthenticationToken);
                    if (role != RolesEnum.Admin)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                            new HttpError("Unauthorized access."));
                    }
                }
                catch (NotFoundException)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                           new HttpError("Unauthorized access."));
                }
            }
        }
    }
}