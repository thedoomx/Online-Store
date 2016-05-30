namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmemberbasket : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StoreBananas", newName: "BananaStores");
            
            DropPrimaryKey("dbo.BananaStores");
            AddPrimaryKey("dbo.BananaStores", new[] { "Banana_Id", "Store_Id" });
           
        }
        
        public override void Down()
        {
            
            DropPrimaryKey("dbo.BananaStores");
            AddPrimaryKey("dbo.BananaStores", new[] { "Store_Id", "Banana_Id" });
            
           
            RenameTable(name: "dbo.BananaStores", newName: "StoreBananas");
        }
    }
}
