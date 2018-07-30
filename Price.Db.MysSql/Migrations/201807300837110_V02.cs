namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V02 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.history", name: "html", newName: "htm");
        }
        
        public override void Down()
        {
            RenameColumn(table: "public.history", name: "htm", newName: "html");
        }
    }
}
