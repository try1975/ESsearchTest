namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V05 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.Contents", name: "ProdStatus2", newName: "ProdStatus");
            RenameColumn(table: "public.history", name: "ProdStatus2", newName: "prod_Status");
        }
        
        public override void Down()
        {
            RenameColumn(table: "public.history", name: "prod_Status", newName: "ProdStatus2");
            RenameColumn(table: "public.Contents", name: "ProdStatus", newName: "ProdStatus2");
        }
    }
}
