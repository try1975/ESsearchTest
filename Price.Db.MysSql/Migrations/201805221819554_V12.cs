namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V12 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Contents", "Screenshot");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contents", new[] { "Screenshot" });
        }
    }
}
