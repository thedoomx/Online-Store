namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmemberbasketapplesbananas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BananaMemberBaskets", "Banana_Id", "dbo.Bananas");
            DropForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets");
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email" });
            DropIndex("dbo.BananaMemberBaskets", new[] { "Banana_Id" });
            DropIndex("dbo.BananaMemberBaskets", new[] { "MemberBasket_Email" });
            RenameColumn(table: "dbo.MemberBasketApples", name: "Apple_Id", newName: "AppleId");
            RenameIndex(table: "dbo.MemberBasketApples", name: "IX_Apple_Id", newName: "IX_AppleId");
            DropPrimaryKey("dbo.MemberBasketApples");
            CreateTable(
                "dbo.MemberBasketBananas",
                c => new
                    {
                        MemberBasketId = c.Int(nullable: false),
                        BananaId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MemberBasket_Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => new { t.MemberBasketId, t.BananaId })
                .ForeignKey("dbo.Bananas", t => t.BananaId, cascadeDelete: true)
                .ForeignKey("dbo.MemberBaskets", t => t.MemberBasket_Email)
                .Index(t => t.BananaId)
                .Index(t => t.MemberBasket_Email);
            
            AddColumn("dbo.MemberBasketApples", "MemberBasketId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberBasketApples", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.MemberBasketApples", "MemberBasket_Email", c => c.String(maxLength: 150));
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasketId", "AppleId" });
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Email");
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets", "Email");
            DropTable("dbo.BananaMemberBaskets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BananaMemberBaskets",
                c => new
                    {
                        Banana_Id = c.Int(nullable: false),
                        MemberBasket_Email = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.Banana_Id, t.MemberBasket_Email });
            
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketBananas", "BananaId", "dbo.Bananas");
            DropIndex("dbo.MemberBasketBananas", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBasketBananas", new[] { "BananaId" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email" });
            DropPrimaryKey("dbo.MemberBasketApples");
            AlterColumn("dbo.MemberBasketApples", "MemberBasket_Email", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.MemberBasketApples", "Quantity");
            DropColumn("dbo.MemberBasketApples", "MemberBasketId");
            DropTable("dbo.MemberBasketBananas");
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasket_Email", "Apple_Id" });
            RenameIndex(table: "dbo.MemberBasketApples", name: "IX_AppleId", newName: "IX_Apple_Id");
            RenameColumn(table: "dbo.MemberBasketApples", name: "AppleId", newName: "Apple_Id");
            CreateIndex("dbo.BananaMemberBaskets", "MemberBasket_Email");
            CreateIndex("dbo.BananaMemberBaskets", "Banana_Id");
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Email");
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets", "Email", cascadeDelete: true);
            AddForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Email", "dbo.MemberBaskets", "Email", cascadeDelete: true);
            AddForeignKey("dbo.BananaMemberBaskets", "Banana_Id", "dbo.Bananas", "Id", cascadeDelete: true);
        }
    }
}
