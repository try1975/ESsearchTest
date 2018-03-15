namespace Price.Db.MysSql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class V01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.history",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    spgz_Id = c.Int(),
                    dt = c.DateTime(nullable: false, precision: 0),
                    price = c.Single(),
                    url = c.String(unicode: false, storeType: "text"),
                    src_id = c.Int(),
                    contact_url = c.String(unicode: false, storeType: "tinytext"),
                    task_id = c.Int(),
                    session_id = c.String(maxLength: 128, storeType: "nvarchar"),
                    preview = c.String(unicode: false),
                    selected = c.Boolean(),
                    screenshot = c.Binary(),
                    currency = c.String(maxLength: 10, storeType: "nvarchar"),
                    opt = c.Int(),
                    referer = c.String(unicode: false, storeType: "text"),
                    prices = c.String(maxLength: 2048, storeType: "nvarchar"),
                    unit_price = c.Single(),
                    unit = c.String(maxLength: 50, storeType: "nvarchar"),
                    weight = c.String(maxLength: 50, storeType: "nvarchar"),
                    rate = c.String(maxLength: 255, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.id)
                .Index(t => t.session_id);

            CreateTable(
                "dbo.Packets",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SearchItems",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Name = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                    InternetSessionId = c.String(maxLength: 128, storeType: "nvarchar"),
                    StartProcessed = c.Long(),
                    LastUpdate = c.Long(),
                    ProcessedAt = c.Long(),
                    Status = c.Int(nullable: false),
                    Source = c.String(maxLength: 128, storeType: "nvarchar"),
                    ExtId = c.String(maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropIndex("dbo.history", new[] { "session_id" });
            DropTable("dbo.SearchItems");
            DropTable("dbo.Packets");
            DropTable("dbo.history");
        }
    }
}
