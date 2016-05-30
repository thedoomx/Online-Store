namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixuser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 202));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 200));
        }
    }
}
