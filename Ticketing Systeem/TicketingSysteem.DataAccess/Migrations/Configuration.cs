namespace TicketingSysteem.DataAccess.Migrations
{
    using Entities.Enums;
    using Entities.PocoFactory;
    using Entities.Pocos;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<TicketingSysteem.DataAccess.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketingSysteem.DataAccess.EntityContext context)
        {

            #region Clean DB
            context.Gebruikers.RemoveRange(context.Gebruikers);
            context.Issue.RemoveRange(context.Issue);
            context.IssueStatus.RemoveRange(context.IssueStatus);
            #endregion

            Gebruiker admin = new Gebruiker()
            {
                Achternaam = "Admin",
                Voornaam = "Jos",
                CreationDate = DateTime.Now,
                Email = "admin@ts.be",
                Rol = Rol.Administrator
            };
            context.Gebruikers.Add(admin);
            context.SaveChanges();

            #region Generate users
            /*
            ICollection<Gebruiker> allUsers = new List<Gebruiker>();
            Gebruiker admin = GebruikerFactory.CreateAdmin();
            allUsers.Add(admin);

            Gebruiker userManager = GebruikerFactory.CreateManager();
            Gebruiker dispatcherManager = GebruikerFactory.CreateManager();
            Gebruiker solverManager = GebruikerFactory.CreateManager();
            
            ICollection<Gebruiker> gebruikers = new List<Gebruiker>();
            foreach (var user in GebruikerFactory.CreateUser(20, userManager))
            {
                gebruikers.Add(user);
                allUsers.Add(user);
            }

            ICollection<Gebruiker> dispatchers = new List<Gebruiker>();
            foreach (var dispatcher in GebruikerFactory.CreaterDispatcher(5, dispatcherManager))
            {
                dispatchers.Add(dispatcher);
                allUsers.Add(dispatcher);
            }
            ICollection<Gebruiker> solvers = new List<Gebruiker>();
            foreach (var solver in GebruikerFactory.CreateSolver(7, solverManager))
            {
                solvers.Add(solver);
                allUsers.Add(solver);
            }
            foreach (var gebruiker in allUsers)
            {
                context.Gebruikers.AddOrUpdate(gebruiker);
            }
            */
            #endregion

            #region Generate Issues
            /*
            ICollection<Issue> issues = IssueFactory.CreateRandomIssues(gebruikers, solvers, 50);
           
            
            foreach (var issue in issues)
            {
                context.Issue.AddOrUpdate(issue);
            }*/
            #endregion
    
        }

    }
}
