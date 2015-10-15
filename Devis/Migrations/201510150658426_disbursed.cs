namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disbursed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.QuoteEntries", "Disbursed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuoteEntries", "Disbursed", c => c.Double());
        }
    }
}
