namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleUsers", "User_Email", "dbo.Users");
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Email" });
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Email = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Email });
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.RoleUsers", "User_Email");
            CreateIndex("dbo.RoleUsers", "Role_Id");
            AddForeignKey("dbo.RoleUsers", "User_Email", "dbo.Users", "Email", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
