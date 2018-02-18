namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchItems", "Normalizer", c => c.String(unicode: false));
            DropColumn("dbo.SearchItems", "Normilizer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SearchItems", "Normilizer", c => c.String(unicode: false));
            DropColumn("dbo.SearchItems", "Normalizer");
        }
    }
}
