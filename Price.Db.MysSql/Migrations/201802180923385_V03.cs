namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchItems", "Normilizer", c => c.String(unicode: false));
            AddColumn("dbo.SearchItems", "JsonText", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchItems", "JsonText");
            DropColumn("dbo.SearchItems", "Normilizer");
        }
    }
}
