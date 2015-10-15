namespace Devis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatedClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Address", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "AdditionalAddress", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "PostalCode", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "City", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "Phone", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "MobilePhone", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "Fax", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "Email", c => c.String(maxLength: 4000));
            AddColumn("dbo.Clients", "Interlocutor", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Interlocutor");
            DropColumn("dbo.Clients", "Email");
            DropColumn("dbo.Clients", "Fax");
            DropColumn("dbo.Clients", "MobilePhone");
            DropColumn("dbo.Clients", "Phone");
            DropColumn("dbo.Clients", "City");
            DropColumn("dbo.Clients", "PostalCode");
            DropColumn("dbo.Clients", "AdditionalAddress");
            DropColumn("dbo.Clients", "Address");
        }
    }
}
