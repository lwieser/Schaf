namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numerotationEnrty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuoteEntries", "Numerotation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuoteEntries", "Numerotation");
        }
    }
}
