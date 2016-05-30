namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetables2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StoreBananas", newName: "BananaStores");
            DropForeignKey("dbo.MemberBasketApples", "AppleId", "dbo.Apples");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketBananas", "BananaId", "dbo.Bananas");
            DropForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email", "dbo.MemberBaskets");
            DropIndex("dbo.MemberBasketApples", new[] { "AppleId" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBasketBananas", new[] { "BananaId" });
            DropIndex("dbo.MemberBasketBananas", new[] { "MemberBasket_Email" });
            DropPrimaryKey("dbo.BananaStores");
            AddPrimaryKey("dbo.BananaStores", new[] { "Banana_Id", "Store_Id" });
            DropTable("dbo.MemberBasketApples");
            DropTable("dbo.MemberBasketBananas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MemberBasketBananas",
                c => new
                    {
                        MemberBasketEmail = c.Int(nullable: false),
                        BananaId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MemberBasket_Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => new { t.MemberBasketEmail, t.BananaId });
            
            CreateTable(
                "dbo.MemberBasketApples",
                c => new
                    {
                        MemberBasketEmail = c.Int(nullable: false),
                        AppleId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MemberBasket_Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => new { t.MemberBasketEmail, t.AppleId });
            
            DropPrimaryKey("dbo.BananaStores");
            AddPrimaryKey("dbo.BananaStores", new[] { "Store_Id", "Banana_Id" });
            CreateIndex("dbo.MemberBasketBananas", "MemberBasket_Email");
            CreateIndex("dbo.MemberBasketBananas", "BananaId");
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Email");
            CreateIndex("dbo.MemberBasketApples", "AppleId");
            AddForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email", "dbo.MemberBaskets", "Email");
            AddForeignKey("dbo.MemberBasketBananas", "BananaId", "dbo.Bananas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets", "Email");
            AddForeignKey("dbo.MemberBasketApples", "AppleId", "dbo.Apples", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.BananaStores", newName: "StoreBananas");
        }
    }
}
