namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBanana : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bananas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfBanana = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stores", "Banana_Id", c => c.Int());
            CreateIndex("dbo.Stores", "Banana_Id");
            AddForeignKey("dbo.Stores", "Banana_Id", "dbo.Bananas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "Banana_Id", "dbo.Bananas");
            DropIndex("dbo.Stores", new[] { "Banana_Id" });
            DropColumn("dbo.Stores", "Banana_Id");
            DropTable("dbo.Bananas");
        }
    }
}
