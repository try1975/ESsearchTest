namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "PriceStatus", c => c.Int(defaultValueSql:"0"));
            AddColumn("dbo.history", "PriceStatus", c => c.Int(defaultValueSql: "0"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.history", "PriceStatus");
            DropColumn("dbo.Contents", "PriceStatus");
        }
    }
}
