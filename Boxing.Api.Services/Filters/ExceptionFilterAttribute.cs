using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Boxing.Core.Services.Exceptions;

namespace Boxing.Api.Services.Filters
{
    public class ExceptionToHttpStatusCodeFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly Dictionary<Type, HttpStatusCode> TypesCodes = new Dictionary<Type, HttpStatusCode>
        {
            [typeof(NotFoundException)] = HttpStatusCode.NotFound,
            [typeof(ArgumentException)] = HttpStatusCode.BadRequest,
            [typeof(ArgumentNullException)] = HttpStatusCode.BadRequest
        };

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode httpStatusCode;
            var isCodeFound = TypesCodes.TryGetValue(actionExecutedContext.Exception.GetType(), out httpStatusCode);
             if (isCodeFound)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(httpStatusCode, new HttpError(actionExecutedContext.Exception.Message));
            }
        }
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            HttpStatusCode httpStatusCode;
            var isCodeFound = TypesCodes.TryGetValue(actionExecutedContext.Exception.GetType(), out httpStatusCode);
            if (isCodeFound)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(httpStatusCode, new HttpError(actionExecutedContext.Exception.Message));
            }
        }
    }
}