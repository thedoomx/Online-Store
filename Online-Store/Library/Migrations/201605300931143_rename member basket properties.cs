namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamememberbasketproperties : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MemberBasketApples", name: "MemberBasketEmail", newName: "Email");
            RenameColumn(table: "dbo.MemberBasketBananas", name: "MemberBasketEmail", newName: "Email");
            RenameIndex(table: "dbo.MemberBasketApples", name: "IX_MemberBasketEmail", newName: "IX_Email");
            RenameIndex(table: "dbo.MemberBasketBananas", name: "IX_MemberBasketEmail", newName: "IX_Email");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MemberBasketBananas", name: "IX_Email", newName: "IX_MemberBasketEmail");
            RenameIndex(table: "dbo.MemberBasketApples", name: "IX_Email", newName: "IX_MemberBasketEmail");
            RenameColumn(table: "dbo.MemberBasketBananas", name: "Email", newName: "MemberBasketEmail");
            RenameColumn(table: "dbo.MemberBasketApples", name: "Email", newName: "MemberBasketEmail");
        }
    }
}
