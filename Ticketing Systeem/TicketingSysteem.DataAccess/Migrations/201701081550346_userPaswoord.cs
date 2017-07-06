namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userPaswoord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gebruiker", "Paswoord", c => c.String(nullable: false));
            AlterColumn("dbo.Gebruiker", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gebruiker", "Email", c => c.String());
            DropColumn("dbo.Gebruiker", "Paswoord");
        }
    }
}
