namespace TicketingSysteem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtraInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueStatusId = c.Int(nullable: false),
                        InfoVraag = c.String(nullable: false),
                        InfoAntwoord = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssueStatus", t => t.IssueStatusId, cascadeDelete: true)
                .Index(t => t.IssueStatusId);
            
            CreateTable(
                "dbo.IssueStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        SolverId = c.Int(nullable: false),
                        AnnulatieReden = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issue", t => t.IssueId, cascadeDelete: true)
                .ForeignKey("dbo.Gebruiker", t => t.SolverId, cascadeDelete: true)
                .Index(t => t.IssueId)
                .Index(t => t.SolverId);
            
            CreateTable(
                "dbo.Issue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false),
                        Beschrijving = c.String(nullable: false),
                        Niveau = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Gebruiker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Voornaam = c.String(nullable: false),
                        Achternaam = c.String(nullable: false),
                        Email = c.String(),
                        TelefoonNummer = c.String(),
                        GsmNummer = c.String(),
                        VerantwoordelijkeId = c.Int(nullable: false),
                        Rol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gebruiker", t => t.VerantwoordelijkeId)
                .Index(t => t.VerantwoordelijkeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtraInfo", "IssueStatusId", "dbo.IssueStatus");
            DropForeignKey("dbo.IssueStatus", "SolverId", "dbo.Gebruiker");
            DropForeignKey("dbo.IssueStatus", "IssueId", "dbo.Issue");
            DropForeignKey("dbo.Issue", "CreatorId", "dbo.Gebruiker");
            DropForeignKey("dbo.Gebruiker", "VerantwoordelijkeId", "dbo.Gebruiker");
            DropIndex("dbo.Gebruiker", new[] { "VerantwoordelijkeId" });
            DropIndex("dbo.Issue", new[] { "CreatorId" });
            DropIndex("dbo.IssueStatus", new[] { "SolverId" });
            DropIndex("dbo.IssueStatus", new[] { "IssueId" });
            DropIndex("dbo.ExtraInfo", new[] { "IssueStatusId" });
            DropTable("dbo.Gebruiker");
            DropTable("dbo.Issue");
            DropTable("dbo.IssueStatus");
            DropTable("dbo.ExtraInfo");
        }
    }
}
