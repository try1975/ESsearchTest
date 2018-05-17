namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchItems", "UserName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchItems", "UserName");
        }
    }
}
