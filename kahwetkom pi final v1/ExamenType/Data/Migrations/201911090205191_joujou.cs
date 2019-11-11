namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joujou : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produits", "Prix", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produits", "Prix");
        }
    }
}
