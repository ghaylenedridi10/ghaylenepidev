namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joujoujou : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produits", "Prix", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produits", "Prix", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
