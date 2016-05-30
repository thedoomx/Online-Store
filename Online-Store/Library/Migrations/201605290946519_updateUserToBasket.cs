namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserToBasket : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "RoleUsers");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Id", "dbo.MemberBaskets");
            DropForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Id", "dbo.MemberBaskets");
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Id" });
            DropIndex("dbo.BananaMemberBaskets", new[] { "MemberBasket_Id" });
            RenameColumn(table: "dbo.MemberBasketApples", name: "MemberBasket_Id", newName: "MemberBasket_Email");
            RenameColumn(table: "dbo.BananaMemberBaskets", name: "MemberBasket_Id", newName: "MemberBasket_Email");
            DropPrimaryKey("dbo.MemberBaskets");
            DropPrimaryKey("dbo.MemberBasketApples");
            DropPrimaryKey("dbo.BananaMemberBaskets");
            DropPrimaryKey("dbo.RoleUsers");
            AddColumn("dbo.MemberBaskets", "Email", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.MemberBasketApples", "MemberBasket_Email", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.BananaMemberBaskets", "MemberBasket_Email", c => c.String(nullable: false, maxLength: 150));
            AddPrimaryKey("dbo.MemberBaskets", "Email");
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasket_Email", "Apple_Id" });
            AddPrimaryKey("dbo.BananaMemberBaskets", new[] { "Banana_Id", "MemberBasket_Email" });
            AddPrimaryKey("dbo.RoleUsers", new[] { "Role_Id", "User_Email" });
            CreateIndex("dbo.MemberBaskets", "Email");
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Email");
            CreateIndex("dbo.BananaMemberBaskets", "MemberBasket_Email");
            AddForeignKey("dbo.MemberBaskets", "Email", "dbo.Users", "Email");
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets", "Email", cascadeDelete: true);
            AddForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Email", "dbo.MemberBaskets", "Email", cascadeDelete: true);
            DropColumn("dbo.MemberBaskets", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberBaskets", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBasketApples", "MemberBasket_Email", "dbo.MemberBaskets");
            DropForeignKey("dbo.MemberBaskets", "Email", "dbo.Users");
            DropIndex("dbo.BananaMemberBaskets", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBasketApples", new[] { "MemberBasket_Email" });
            DropIndex("dbo.MemberBaskets", new[] { "Email" });
            DropPrimaryKey("dbo.RoleUsers");
            DropPrimaryKey("dbo.BananaMemberBaskets");
            DropPrimaryKey("dbo.MemberBasketApples");
            DropPrimaryKey("dbo.MemberBaskets");
            AlterColumn("dbo.BananaMemberBaskets", "MemberBasket_Email", c => c.Int(nullable: false));
            AlterColumn("dbo.MemberBasketApples", "MemberBasket_Email", c => c.Int(nullable: false));
            DropColumn("dbo.MemberBaskets", "Email");
            AddPrimaryKey("dbo.RoleUsers", new[] { "User_Email", "Role_Id" });
            AddPrimaryKey("dbo.BananaMemberBaskets", new[] { "Banana_Id", "MemberBasket_Id" });
            AddPrimaryKey("dbo.MemberBasketApples", new[] { "MemberBasket_Id", "Apple_Id" });
            AddPrimaryKey("dbo.MemberBaskets", "Id");
            RenameColumn(table: "dbo.BananaMemberBaskets", name: "MemberBasket_Email", newName: "MemberBasket_Id");
            RenameColumn(table: "dbo.MemberBasketApples", name: "MemberBasket_Email", newName: "MemberBasket_Id");
            CreateIndex("dbo.BananaMemberBaskets", "MemberBasket_Id");
            CreateIndex("dbo.MemberBasketApples", "MemberBasket_Id");
            AddForeignKey("dbo.BananaMemberBaskets", "MemberBasket_Id", "dbo.MemberBaskets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MemberBasketApples", "MemberBasket_Id", "dbo.MemberBaskets", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.RoleUsers", newName: "UserRoles");
        }
    }
}
