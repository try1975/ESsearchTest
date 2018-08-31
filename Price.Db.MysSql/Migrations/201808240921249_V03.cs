namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class V03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Contents", "RejectReason", c => c.String(maxLength: 128));
            AddColumn("public.Contents", "ManualPrice", c => c.Int(defaultValue: 0));
            AddColumn("public.history", "RejectReason", c => c.String(maxLength: 128));
            AddColumn("public.history", "ManualPrice", c => c.Int(defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("public.history", "ManualPrice");
            DropColumn("public.history", "RejectReason");
            DropColumn("public.Contents", "ManualPrice");
            DropColumn("public.Contents", "RejectReason");
        }
    }
}
