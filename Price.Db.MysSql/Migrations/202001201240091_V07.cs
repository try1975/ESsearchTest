namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V07 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstDate = c.DateTime(nullable: false),
                        LastDate = c.DateTime(),
                        NextRequestDate = c.DateTime(),
                        Frequency = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("public.Schedules");
        }
    }
}
