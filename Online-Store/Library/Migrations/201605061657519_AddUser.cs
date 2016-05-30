namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 150),
                        UserId = c.Guid(nullable: false, identity: true),
                        Password = c.String(maxLength: 200),
                        PasswordSalt = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Email)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.Users");
        }
    }
}
