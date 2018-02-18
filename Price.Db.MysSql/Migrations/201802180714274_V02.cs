namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Price = c.String(maxLength: 128, storeType: "nvarchar"),
                        Uri = c.String(unicode: false),
                        CollectedAt = c.Long(),
                        ElasticId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Okpd2 = c.String(maxLength: 128, storeType: "nvarchar"),
                        SearchItemId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SearchItems", t => t.SearchItemId, cascadeDelete: true)
                .Index(t => t.ElasticId)
                .Index(t => t.SearchItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "SearchItemId", "dbo.SearchItems");
            DropIndex("dbo.Contents", new[] { "SearchItemId" });
            DropIndex("dbo.Contents", new[] { "ElasticId" });
            DropTable("dbo.Contents");
        }
    }
}
