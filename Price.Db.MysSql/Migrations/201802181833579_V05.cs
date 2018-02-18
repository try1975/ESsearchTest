namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V05 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SearchItems", "Name");
            CreateIndex("dbo.SearchItems", "StartProcessed");
            CreateIndex("dbo.SearchItems", "ExtId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SearchItems", new[] { "ExtId" });
            DropIndex("dbo.SearchItems", new[] { "StartProcessed" });
            DropIndex("dbo.SearchItems", new[] { "Name" });
        }
    }
}
