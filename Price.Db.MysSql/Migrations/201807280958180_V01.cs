namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.String(maxLength: 128),
                        Uri = c.String(),
                        CollectedAt = c.Long(),
                        ElasticId = c.String(nullable: false, maxLength: 128),
                        Okpd2 = c.String(maxLength: 128),
                        SearchItemId = c.String(nullable: false, maxLength: 128),
                        Screenshot = c.String(maxLength: 128),
                        PriceStatus = c.Int(),
                        Seller = c.String(),
                        Producer = c.String(),
                        Phones = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.SearchItems", t => t.SearchItemId, cascadeDelete: true)
                .Index(t => t.ElasticId)
                .Index(t => t.SearchItemId)
                .Index(t => t.Screenshot);
            
            CreateTable(
                "public.SearchItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 250),
                        InternetSessionId = c.String(maxLength: 128),
                        StartProcessed = c.Long(),
                        LastUpdate = c.Long(),
                        ProcessedAt = c.Long(),
                        Status = c.Int(nullable: false),
                        Source = c.String(maxLength: 128),
                        ExtId = c.String(maxLength: 128),
                        Normalizer = c.String(),
                        JsonText = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name)
                .Index(t => t.StartProcessed)
                .Index(t => t.ExtId);
            
            CreateTable(
                "public.history",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        spgz_Id = c.Int(),
                        dt = c.DateTime(nullable: false),
                        price = c.Single(),
                        url = c.String(),
                        src_id = c.Int(),
                        contact_url = c.String(maxLength: 128),
                        task_id = c.Int(),
                        session_id = c.String(maxLength: 128),
                        preview = c.String(),
                        selected = c.Int(),
                        screenshot = c.Binary(),
                        currency = c.String(maxLength: 10),
                        opt = c.Int(),
                        referer = c.String(),
                        prices = c.String(maxLength: 2048),
                        unit_price = c.Single(),
                        unit = c.String(maxLength: 50),
                        weight = c.String(maxLength: 50),
                        rate = c.String(maxLength: 255),
                        txt = c.String(),
                        html = c.String(),
                        upd = c.Int(),
                        PriceStatus = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.dt)
                .Index(t => t.contact_url)
                .Index(t => t.session_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Contents", "SearchItemId", "public.SearchItems");
            DropIndex("public.history", new[] { "session_id" });
            DropIndex("public.history", new[] { "contact_url" });
            DropIndex("public.history", new[] { "dt" });
            DropIndex("public.SearchItems", new[] { "ExtId" });
            DropIndex("public.SearchItems", new[] { "StartProcessed" });
            DropIndex("public.SearchItems", new[] { "Name" });
            DropIndex("public.Contents", new[] { "Screenshot" });
            DropIndex("public.Contents", new[] { "SearchItemId" });
            DropIndex("public.Contents", new[] { "ElasticId" });
            DropTable("public.history");
            DropTable("public.SearchItems");
            DropTable("public.Contents");
        }
    }
}
