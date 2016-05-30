namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoreBananas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stores", "Banana_Id", "dbo.Bananas");
            DropIndex("dbo.Stores", new[] { "Banana_Id" });
            CreateTable(
                "dbo.BananaStores",
                c => new
                    {
                        Banana_Id = c.Int(nullable: false),
                        Store_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Banana_Id, t.Store_Id })
                .ForeignKey("dbo.Bananas", t => t.Banana_Id, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .Index(t => t.Banana_Id)
                .Index(t => t.Store_Id);
            
            DropColumn("dbo.Stores", "Banana_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "Banana_Id", c => c.Int());
            DropForeignKey("dbo.BananaStores", "Store_Id", "dbo.Stores");
            DropForeignKey("dbo.BananaStores", "Banana_Id", "dbo.Bananas");
            DropIndex("dbo.BananaStores", new[] { "Store_Id" });
            DropIndex("dbo.BananaStores", new[] { "Banana_Id" });
            DropTable("dbo.BananaStores");
            CreateIndex("dbo.Stores", "Banana_Id");
            AddForeignKey("dbo.Stores", "Banana_Id", "dbo.Bananas", "Id");
        }
    }
}
