using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TicketingSysteem.Models;
using TicketingSysteem.Entities.Enums;
using System;
using System.Linq;
using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Entities.Pocos;
using Microsoft.Practices.Unity;

namespace TicketingSysteem
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        [Dependency]
        public IReader<Gebruiker> _gebruikerReader { get; set; }

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var users = await _gebruikerReader.Filter(g => g.Email == userId);
            return users?.FirstOrDefault()?.Rol.ToString() == role;
        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));

            return appRoleManager;
        }

        public override Task<bool> RoleExistsAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return Task.FromResult<bool>(false);
            if (Enum.GetNames(typeof(Rol)).Contains(roleName)) return Task.FromResult<bool>(true);
            return Task.FromResult<bool>(false);
        }

    }
}
