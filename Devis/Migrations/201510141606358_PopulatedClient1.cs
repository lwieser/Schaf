namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatedClient1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuoteArticles", "Disbursed", c => c.Double(nullable: false));
            AlterColumn("dbo.QuoteEntries", "Disbursed", c => c.Double(nullable: false));
            AlterColumn("dbo.QuotePackages", "Disbursed", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuotePackages", "Disbursed", c => c.Int(nullable: false));
            AlterColumn("dbo.QuoteEntries", "Disbursed", c => c.Int(nullable: false));
            AlterColumn("dbo.QuoteArticles", "Disbursed", c => c.Int(nullable: false));
        }
    }
}
