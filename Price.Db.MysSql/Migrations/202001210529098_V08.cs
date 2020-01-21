namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Schedules", "Name", c => c.String());
            AddColumn("public.Schedules", "Uri", c => c.String());
            DropColumn("public.Schedules", "Note");
        }
        
        public override void Down()
        {
            AddColumn("public.Schedules", "Note", c => c.String());
            DropColumn("public.Schedules", "Uri");
            DropColumn("public.Schedules", "Name");
        }
    }
}
