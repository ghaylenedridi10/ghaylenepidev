namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Promotions", new[] { "Products_Id_produit" });
            DropColumn("dbo.Promotions", "IdProduit");
            RenameColumn(table: "dbo.Promotions", name: "Products_Id_produit", newName: "IdProduit");
            AlterColumn("dbo.Promotions", "IdProduit", c => c.Int());
            CreateIndex("dbo.Promotions", "IdProduit");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Promotions", new[] { "IdProduit" });
            AlterColumn("dbo.Promotions", "IdProduit", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Promotions", name: "IdProduit", newName: "Products_Id_produit");
            AddColumn("dbo.Promotions", "IdProduit", c => c.Int(nullable: false));
            CreateIndex("dbo.Promotions", "Products_Id_produit");
        }
    }
}
