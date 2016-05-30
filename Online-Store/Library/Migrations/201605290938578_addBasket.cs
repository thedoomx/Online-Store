namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBasket : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BananaStores", newName: "StoreBananas");
            DropPrimaryKey("dbo.StoreBananas");
            CreateTable(
                "dbo.MemberBaskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberBasketApples",
                c => new
                    {
                        MemberBasket_Id = c.Int(nullable: false),
                        Apple_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberBasket_Id, t.Apple_Id })
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasket_Id, cascadeDelete: true)
                .ForeignKey("dbo.Apples", t => t.Apple_Id, cascadeDelete: true)
                .Index(t => t.MemberBasket_Id)
                .Index(t => t.Apple_Id);
            
            CreateTable(
                "dbo.BananaMemberBaskets",
                c => new
                    {
                        Banana_Id = c.Int(nullable: false),
                        MemberBasket_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Banana_Id, t.MemberBasket_Id })
                .ForeignKey("dbo.Bananas", t => t.Banana_Id, cascadeDelete: true)
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasket_Id, cascadeDelete: true)
                .Index(t => t.Banana_Id)
                .Index(t => t.MemberBasket_Id);
            
            AddPrimaryKey("dbo.StoreBananas", new[] { "Store_Id", "Banana_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Id", "dbo.MemberBaskets");
            DropForeignKey("dbo.BananaMemberBaskets", "Banana_Id", "dbo.Bananas");
            DropForeignKey("dbo.MemberBasketApples", "Apple_Id", "dbo.Apples");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Id", "dbo.MemberBaskets");
            DropIndex("dbo.BananaMemberBaskets", new[] { "MemberBasket_Id" });
            DropIndex("dbo.BananaMemberBaskets", new[] { "Banana_Id" });
            DropIndex("dbo.MemberBasketApples", new[] { "Apple_Id" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Id" });
            DropPrimaryKey("dbo.StoreBananas");
            DropTable("dbo.BananaMemberBaskets");
            DropTable("dbo.MemberBasketApples");
            DropTable("dbo.MemberBaskets");
            AddPrimaryKey("dbo.StoreBananas", new[] { "Banana_Id", "Store_Id" });
            RenameTable(name: "dbo.StoreBananas", newName: "BananaStores");
        }
    }
}
