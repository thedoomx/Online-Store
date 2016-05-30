namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmemberbaskettables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BananaStores", newName: "StoreBananas");
            DropPrimaryKey("dbo.StoreBananas");
            CreateTable(
                "dbo.MemberBasketApples",
                c => new
                    {
                        MemberBasketEmail = c.String(nullable: false, maxLength: 150),
                        AppleId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberBasketEmail, t.AppleId })
                .ForeignKey("dbo.Apples", t => t.AppleId, cascadeDelete: true)
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasketEmail, cascadeDelete: true)
                .Index(t => t.MemberBasketEmail)
                .Index(t => t.AppleId);
            
            CreateTable(
                "dbo.MemberBasketBananas",
                c => new
                    {
                        MemberBasketEmail = c.String(nullable: false, maxLength: 150),
                        BananaId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberBasketEmail, t.BananaId })
                .ForeignKey("dbo.Bananas", t => t.BananaId, cascadeDelete: true)
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasketEmail, cascadeDelete: true)
                .Index(t => t.MemberBasketEmail)
                .Index(t => t.BananaId);
            
            AddPrimaryKey("dbo.StoreBananas", new[] { "Store_Id", "Banana_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberBasketBananas", "MemberBasketEmail", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketBananas", "BananaId", "dbo.Bananas");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasketEmail", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketApples", "AppleId", "dbo.Apples");
            DropIndex("dbo.MemberBasketBananas", new[] { "BananaId" });
            DropIndex("dbo.MemberBasketBananas", new[] { "MemberBasketEmail" });
            DropIndex("dbo.MemberBasketApples", new[] { "AppleId" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasketEmail" });
            DropPrimaryKey("dbo.StoreBananas");
            DropTable("dbo.MemberBasketBananas");
            DropTable("dbo.MemberBasketApples");
            AddPrimaryKey("dbo.StoreBananas", new[] { "Banana_Id", "Store_Id" });
            RenameTable(name: "dbo.StoreBananas", newName: "BananaStores");
        }
    }
}
