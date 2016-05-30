namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfApple = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoreApples",
                c => new
                    {
                        Store_Id = c.Int(nullable: false),
                        Apple_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Store_Id, t.Apple_Id })
                .ForeignKey("dbo.Stores", t => t.Store_Id, cascadeDelete: true)
                .ForeignKey("dbo.Apples", t => t.Apple_Id, cascadeDelete: true)
                .Index(t => t.Store_Id)
                .Index(t => t.Apple_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreApples", "Apple_Id", "dbo.Apples");
            DropForeignKey("dbo.StoreApples", "Store_Id", "dbo.Stores");
            DropIndex("dbo.StoreApples", new[] { "Apple_Id" });
            DropIndex("dbo.StoreApples", new[] { "Store_Id" });
            DropTable("dbo.StoreApples");
            DropTable("dbo.Stores");
            DropTable("dbo.Apples");
        }
    }
}
