namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 4000),
                        Date = c.DateTime(),
                        RevivalDate = c.DateTime(),
                        CodeC = c.Long(),
                        OwnerCode = c.String(maxLength: 4000),
                        Subject = c.String(maxLength: 4000),
                        State = c.Int(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Civility_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Civilities", t => t.Civility_Id)
                .Index(t => t.Civility_Id);
            
            CreateTable(
                "dbo.Civilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuotePackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        Reference = c.String(maxLength: 4000),
                        Label = c.String(maxLength: 4000),
                        Progress = c.Double(),
                        Quantity = c.Int(),
                        Unit = c.String(maxLength: 4000),
                        Amount = c.Double(),
                        Quote_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotes", t => t.Quote_Id)
                .Index(t => t.Quote_Id);
            
            CreateTable(
                "dbo.QuoteEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        Reference = c.String(maxLength: 4000),
                        Label = c.String(maxLength: 4000),
                        Progress = c.Double(),
                        Quantity = c.Int(),
                        Unit = c.String(maxLength: 4000),
                        Amount = c.Double(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuotePackages", t => t.Package_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.QuoteArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reference = c.String(maxLength: 4000),
                        Label = c.String(maxLength: 4000),
                        Progress = c.Double(),
                        Price = c.Double(),
                        Quantity = c.Int(),
                        Unit = c.String(maxLength: 4000),
                        Amount = c.Double(),
                        Entry_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuoteEntries", t => t.Entry_Id)
                .Index(t => t.Entry_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotePackages", "Quote_Id", "dbo.Quotes");
            DropForeignKey("dbo.QuoteEntries", "Package_Id", "dbo.QuotePackages");
            DropForeignKey("dbo.QuoteArticles", "Entry_Id", "dbo.QuoteEntries");
            DropForeignKey("dbo.Quotes", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "Civility_Id", "dbo.Civilities");
            DropIndex("dbo.QuoteArticles", new[] { "Entry_Id" });
            DropIndex("dbo.QuoteEntries", new[] { "Package_Id" });
            DropIndex("dbo.QuotePackages", new[] { "Quote_Id" });
            DropIndex("dbo.Clients", new[] { "Civility_Id" });
            DropIndex("dbo.Quotes", new[] { "Client_Id" });
            DropTable("dbo.QuoteArticles");
            DropTable("dbo.QuoteEntries");
            DropTable("dbo.QuotePackages");
            DropTable("dbo.Civilities");
            DropTable("dbo.Clients");
            DropTable("dbo.Quotes");
        }
    }
}
