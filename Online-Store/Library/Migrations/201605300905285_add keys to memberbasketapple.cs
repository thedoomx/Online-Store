namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addkeystomemberbasketapple : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MemberBasketApples");
            DropPrimaryKey("dbo.MemberBasketBananas");
            AddColumn("dbo.MemberBasketApples", "MemberBasketEmail", c => c.Int(nullable: false));
            AddColumn("dbo.MemberBasketBananas", "MemberBasketEmail", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasketEmail", "AppleId" });
            AddPrimaryKey("dbo.MemberBasketBananas", new[] { "MemberBasketEmail", "BananaId" });
            DropColumn("dbo.MemberBasketApples", "MemberBasketId");
            DropColumn("dbo.MemberBasketBananas", "MemberBasketId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberBasketBananas", "MemberBasketId", c => c.Int(nullable: false));
            AddColumn("dbo.MemberBasketApples", "MemberBasketId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.MemberBasketBananas");
            DropPrimaryKey("dbo.MemberBasketApples");
            DropColumn("dbo.MemberBasketBananas", "MemberBasketEmail");
            DropColumn("dbo.MemberBasketApples", "MemberBasketEmail");
            AddPrimaryKey("dbo.MemberBasketBananas", new[] { "MemberBasketId", "BananaId" });
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasketId", "AppleId" });
        }
    }
}
