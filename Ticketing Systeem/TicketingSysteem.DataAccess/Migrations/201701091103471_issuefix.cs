namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issuefix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Issue", name: "CreatorId", newName: "GebruikerId");
            RenameIndex(table: "dbo.Issue", name: "IX_CreatorId", newName: "IX_GebruikerId");
            AddColumn("dbo.IssueStatus", "StatusBeschrijving", c => c.Int(nullable: false));
            AddColumn("dbo.Issue", "Oplossing", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issue", "Oplossing");
            DropColumn("dbo.IssueStatus", "StatusBeschrijving");
            RenameIndex(table: "dbo.Issue", name: "IX_GebruikerId", newName: "IX_CreatorId");
            RenameColumn(table: "dbo.Issue", name: "GebruikerId", newName: "CreatorId");
        }
    }
}
