namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmemberbaskets2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email", "dbo.MemberBaskets");
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBasketBananas", new[] { "MemberBasket_Email" });
            DropPrimaryKey("dbo.MemberBasketApples");
            DropPrimaryKey("dbo.MemberBasketBananas");
            AddColumn("dbo.MemberBasketApples", "MemberBasket_Email1", c => c.String(maxLength: 150));
            AddColumn("dbo.MemberBasketBananas", "MemberBasket_Email1", c => c.String(maxLength: 150));
            AlterColumn("dbo.MemberBasketApples", "MemberBasket_Email", c => c.Int(nullable: false));
            AlterColumn("dbo.MemberBasketBananas", "MemberBasket_Email", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasket_Email", "AppleId" });
            AddPrimaryKey("dbo.MemberBasketBananas", new[] { "MemberBasket_Email", "BananaId" });
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Email1");
            CreateIndex("dbo.MemberBasketBananas", "MemberBasket_Email1");
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Email1", "dbo.MemberBaskets", "Email");
            AddForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email1", "dbo.MemberBaskets", "Email");
            DropColumn("dbo.MemberBasketApples", "MemberBasketEmail");
            DropColumn("dbo.MemberBasketBananas", "MemberBasketEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberBasketBananas", "MemberBasketEmail", c => c.Int(nullable: false));
            AddColumn("dbo.MemberBasketApples", "MemberBasketEmail", c => c.Int(nullable: false));
            DropForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email1", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email1", "dbo.MemberBaskets");
            DropIndex("dbo.MemberBasketBananas", new[] { "MemberBasket_Email1" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email1" });
            DropPrimaryKey("dbo.MemberBasketBananas");
            DropPrimaryKey("dbo.MemberBasketApples");
            AlterColumn("dbo.MemberBasketBananas", "MemberBasket_Email", c => c.String(maxLength: 150));
            AlterColumn("dbo.MemberBasketApples", "MemberBasket_Email", c => c.String(maxLength: 150));
            DropColumn("dbo.MemberBasketBananas", "MemberBasket_Email1");
            DropColumn("dbo.MemberBasketApples", "MemberBasket_Email1");
            AddPrimaryKey("dbo.MemberBasketBananas", new[] { "MemberBasketEmail", "BananaId" });
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasketEmail", "AppleId" });
            CreateIndex("dbo.MemberBasketBananas", "MemberBasket_Email");
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Email");
            AddForeignKey("dbo.MemberBasketBananas", "MemberBasket_Email", "dbo.MemberBaskets", "Email");
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets", "Email");
        }
    }
}
