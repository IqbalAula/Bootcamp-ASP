namespace BootcampASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatenot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemVMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Suppliers_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ItemVMs");
        }
    }
}
