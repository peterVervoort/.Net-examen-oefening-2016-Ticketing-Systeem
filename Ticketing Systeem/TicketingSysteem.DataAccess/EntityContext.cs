using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.DataAccess
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base()
        {

        }

        #region DBset
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<IssueStatus> IssueStatus { get; set; }
        public DbSet<ExtraInfo> ExtraInfo { get; set; }
        #endregion
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Issue>()
                .HasRequired(i => i.Gebruiker)
                .WithMany(g => g.Issues)
                .HasForeignKey(i => i.GebruikerId)
                .WillCascadeOnDelete(true);

        }

    }
}
