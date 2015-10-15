namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class convertQuantityToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuoteArticles", "Quantity", c => c.Double());
            AlterColumn("dbo.QuoteEntries", "Quantity", c => c.Double());
            AlterColumn("dbo.QuotePackages", "Quantity", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QuotePackages", "Quantity", c => c.Int());
            AlterColumn("dbo.QuoteEntries", "Quantity", c => c.Int());
            AlterColumn("dbo.QuoteArticles", "Quantity", c => c.Int());
        }
    }
}
