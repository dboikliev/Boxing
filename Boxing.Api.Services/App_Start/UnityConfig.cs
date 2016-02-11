using Microsoft.Practices.Unity;
using System.Web.Http;
using Boxing.Core.Services.Implementations;
using Boxing.Core.Services.Interfaces;
using Unity.WebApi;

namespace Boxing.Api.Services
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IMatchesService, MatchesService>();
            container.RegisterType<ILoginsService, LoginsService>();
            container.RegisterType<IRolesService, RolesService>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}