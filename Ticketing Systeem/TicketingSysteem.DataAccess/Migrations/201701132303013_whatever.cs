namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatever : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issue", "GebruikerId", "dbo.Gebruiker");
            AddForeignKey("dbo.Issue", "GebruikerId", "dbo.Gebruiker", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issue", "GebruikerId", "dbo.Gebruiker");
            AddForeignKey("dbo.Issue", "GebruikerId", "dbo.Gebruiker", "Id");
        }
    }
}
