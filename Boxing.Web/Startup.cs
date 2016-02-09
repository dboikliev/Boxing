using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Boxing.Web.Startup))]
namespace Boxing.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
