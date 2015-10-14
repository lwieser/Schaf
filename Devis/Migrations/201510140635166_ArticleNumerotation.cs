namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleNumerotation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuoteArticles", "Numerotation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuoteArticles", "Numerotation");
        }
    }
}
