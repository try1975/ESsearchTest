namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "Seller", c => c.String(unicode: false));
            AddColumn("dbo.Contents", "Producer", c => c.String(unicode: false));
            AddColumn("dbo.Contents", "Phones", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contents", "Phones");
            DropColumn("dbo.Contents", "Producer");
            DropColumn("dbo.Contents", "Seller");
        }
    }
}
