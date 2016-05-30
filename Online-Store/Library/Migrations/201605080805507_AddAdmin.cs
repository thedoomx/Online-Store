namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Administrator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Administrator");
        }
    }
}
