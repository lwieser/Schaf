namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disbursed1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.QuotePackages", "Disbursed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuotePackages", "Disbursed", c => c.Double());
        }
    }
}
