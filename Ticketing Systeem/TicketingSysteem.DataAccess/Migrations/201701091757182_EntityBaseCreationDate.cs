namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityBaseCreationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gebruiker", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gebruiker", "CreationDate");
        }
    }
}
