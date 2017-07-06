namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userVerantwoordelijkeNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Gebruiker", new[] { "VerantwoordelijkeId" });
            AlterColumn("dbo.Gebruiker", "VerantwoordelijkeId", c => c.Int());
            CreateIndex("dbo.Gebruiker", "VerantwoordelijkeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Gebruiker", new[] { "VerantwoordelijkeId" });
            AlterColumn("dbo.Gebruiker", "VerantwoordelijkeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Gebruiker", "VerantwoordelijkeId");
        }
    }
}
