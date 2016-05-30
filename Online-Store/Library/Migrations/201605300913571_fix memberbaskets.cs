namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmemberbaskets : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BananaStores", newName: "StoreBananas");
            DropPrimaryKey("dbo.StoreBananas");
            CreateTable(
                "dbo.MemberBasketApples",
                c => new
                    {
                        MemberBasketEmail = c.Int(nullable: false),
                        AppleId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MemberBasket_Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => new { t.MemberBasketEmail, t.AppleId })
                .ForeignKey("dbo.Apples", t => t.AppleId, cascadeDelete: true)
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasket_Email)
                .Index(t => t.AppleId)
                .Index(t => t.MemberBasket_Email);
            
            CreateTable(
                "dbo.MemberBasketBananas",
                c => new
                    {
                        MemberBasketEmail = c.Int(nullable: false),
                        BananaId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MemberBasket_Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => new { t.MemberBasketEmail, t.BananaId })
                .ForeignKey("dbo.Bananas", t => t.BananaId, cascadeDelete: true)
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasket_Email)
                .Index(t => t.BananaId)
                .Index(t => t.MemberBasket_Email);
            
            AddPrimaryKey("dbo.StoreBananas", new[] { "Store_Id", "Banana_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketBananas", "BananaId", "dbo.Bananas");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketApples", "AppleId", "dbo.Apples");
            DropIndex("dbo.MemberBasketBananas", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBasketBananas", new[] { "BananaId" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBasketApples", new[] { "AppleId" });
            DropPrimaryKey("dbo.StoreBananas");
            DropTable("dbo.MemberBasketBananas");
            DropTable("dbo.MemberBasketApples");
            AddPrimaryKey("dbo.StoreBananas", new[] { "Banana_Id", "Store_Id" });
            RenameTable(name: "dbo.StoreBananas", newName: "BananaStores");
        }
    }
}
