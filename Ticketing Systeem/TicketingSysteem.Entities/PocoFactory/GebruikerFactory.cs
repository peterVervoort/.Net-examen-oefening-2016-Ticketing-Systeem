using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.PocoFactory.FactoryUsedEnums;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.PocoFactory
{
    public class GebruikerFactory
    {
        public static ICollection<Gebruiker> CreateRandomGebruikers(int users, Gebruiker userManager, int dispatchers, Gebruiker dispatcherManager, int solvers, Gebruiker solverManager)
        {
            ICollection<Gebruiker> gebruikers = new List<Gebruiker>();
            foreach (var user in CreateUser(users, userManager))
            {
                gebruikers.Add(user);
            }
            foreach(var dispatcher in CreaterDispatcher(dispatchers, dispatcherManager))
            {
                gebruikers.Add(dispatcher);
            }
            foreach(var solver in CreateSolver(solvers, solverManager))
            {
                gebruikers.Add(solver);
            }
            gebruikers.Add(CreateAdmin());
            return gebruikers;
        }

        private static Gebruiker CreateGebruiker(int i = 0)
        {
            Gebruiker gebruiker = new Gebruiker();
            var random = new Random(i);
            gebruiker.Achternaam = Enum.GetName(typeof(Achternaam), random.Next(Enum.GetNames(typeof(Achternaam)).Length));
            gebruiker.Voornaam = Enum.GetName(typeof(Voornaam), random.Next(Enum.GetNames(typeof(Voornaam)).Length));
            gebruiker.Email = gebruiker.Voornaam + "." + gebruiker.Achternaam + "@gmail.com";
            gebruiker.CreationDate = DateTime.UtcNow;
            return gebruiker;
        }

       
        public static Gebruiker CreateAdmin(int i = 0)
        {
            Gebruiker admin = CreateGebruiker(i);
            admin.Rol = Rol.Administrator;
            return admin;
        }

        public static Gebruiker CreateManager(int i = 0)
        {
            Gebruiker manager = CreateGebruiker();
            manager.Rol = Rol.Manager;
            return manager;
        }
        public static ICollection<Gebruiker> CreateUser(int aantal, Gebruiker manager)
        {
            Rol rol = Rol.Gebruiker;
            ICollection<Gebruiker> gebruikers = CreateManagerAndRole(rol, aantal, manager);
            return gebruikers;
        }
        public static ICollection<Gebruiker> CreaterDispatcher(int aantal, Gebruiker manager)
        {
            Rol rol = Rol.Dispatcher;
            ICollection<Gebruiker> gebruikers = CreateManagerAndRole(rol, aantal, manager);
            return gebruikers;
        }

        public static ICollection<Gebruiker> CreateSolver(int aantal, Gebruiker manager)
        {
            Rol rol = Rol.Solver;
            ICollection<Gebruiker> gebruikers = CreateManagerAndRole(rol, aantal, manager);
            return gebruikers;
        }

        private static ICollection<Gebruiker> CreateManagerAndRole(Rol rol, int aantal, Gebruiker manager)
        {
            ICollection<Gebruiker> gebruikers = new List<Gebruiker>();
            for (int i = 0; i <= aantal; i++)
            {
                Gebruiker user = CreateGebruiker(i);
                user.Verantwoordelijke = manager;
                user.Rol = rol;
                gebruikers.Add(user);
            }
            return gebruikers;
        }

    }
}
