namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Contents", "ProdStatus2", c => c.Int());
            AddColumn("public.history", "ProdStatus2", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("public.history", "ProdStatus2");
            DropColumn("public.Contents", "ProdStatus2");
        }
    }
}
