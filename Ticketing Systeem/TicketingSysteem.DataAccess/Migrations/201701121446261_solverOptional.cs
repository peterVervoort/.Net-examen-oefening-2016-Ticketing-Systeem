namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solverOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IssueStatus", "SolverId", "dbo.Gebruiker");
            DropIndex("dbo.IssueStatus", new[] { "SolverId" });
            AlterColumn("dbo.IssueStatus", "SolverId", c => c.Int());
            CreateIndex("dbo.IssueStatus", "SolverId");
            AddForeignKey("dbo.IssueStatus", "SolverId", "dbo.Gebruiker", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueStatus", "SolverId", "dbo.Gebruiker");
            DropIndex("dbo.IssueStatus", new[] { "SolverId" });
            AlterColumn("dbo.IssueStatus", "SolverId", c => c.Int(nullable: false));
            CreateIndex("dbo.IssueStatus", "SolverId");
            AddForeignKey("dbo.IssueStatus", "SolverId", "dbo.Gebruiker", "Id", cascadeDelete: true);
        }
    }
}
