namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V06 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Packets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Packets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
