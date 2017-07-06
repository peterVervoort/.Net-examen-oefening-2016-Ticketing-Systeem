using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System.Web.Http;
using TicketingSysteem.Controllers;
using TicketingSysteem.Models;
using Unity.WebApi;

namespace TicketingSysteem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            Business.Factory.Configure(container);

            //container.RegisterType<AccountController>(new InjectionConstructor());

            //var accountInjectionConstructor = new InjectionConstructor(new IdentitySampleDbModelContext(configurationStore));
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(accountInjectionConstructor);
            //container.RegisterType<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>(accountInjectionConstructor);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}