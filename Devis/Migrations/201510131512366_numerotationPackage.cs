namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numerotationPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuotePackages", "Numerotation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuotePackages", "Numerotation");
        }
    }
}
