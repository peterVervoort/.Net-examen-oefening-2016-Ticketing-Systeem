namespace TicketingSysteem.Migrations
{
    using Entities.Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketingSysteem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TicketingSysteem.Models.ApplicationDbContext";
        }

        protected override void Seed(TicketingSysteem.Models.ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            foreach (string rol in Enum.GetNames(typeof(Rol)))
            {
                if (!context.Roles.Any(r => r.Name == rol.ToString()))
                {
                    var role = new IdentityRole { Name = rol.ToString() };

                    manager.Create(role);
                }
            }

            if (!context.Users.Any(u => u.UserName == "admin@ts.be"))
            {
                var userstore = new UserStore<ApplicationUser>(context);
                var usermanager = new UserManager<ApplicationUser>(userstore);
                var user = new ApplicationUser { UserName = "admin@ts.be", Email = "admin@ts.be" };

                usermanager.Create(user, "admin123");
                usermanager.AddToRole(user.Id, Rol.Administrator.ToString());
            }



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
