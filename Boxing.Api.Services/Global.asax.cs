using System.Web.Http;

namespace Boxing.Api.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
                UnityConfig.Register(config);
                FilterConfig.Register(config);
                config.EnsureInitialized();
            });
            
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //GlobalConfiguration.Configuration.EnsureInitialized();

            //GlobalConfiguration.Configure(UnityConfig.Register);
            //GlobalConfiguration.Configure(FilterConfig.Register);
        }
    }
}
