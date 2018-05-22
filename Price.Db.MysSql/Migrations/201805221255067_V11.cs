namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.history", "contact_url", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("dbo.history", "dt");
            CreateIndex("dbo.history", "contact_url");
        }
        
        public override void Down()
        {
            DropIndex("dbo.history", new[] { "contact_url" });
            DropIndex("dbo.history", new[] { "dt" });
            AlterColumn("dbo.history", "contact_url", c => c.String(unicode: false, storeType: "tinytext"));
        }
    }
}
