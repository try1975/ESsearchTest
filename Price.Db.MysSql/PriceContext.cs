using System.Data.Entity;
using Price.Db.Entities.Entities;
using Price.Db.Postgress.Mappings;
using Price.Db.Postgress.Migrations;

namespace Price.Db.Postgress
{
    public class PriceContext : DbContext
    {
        public PriceContext() : base("name=PriceConnection")
        {
            //Database.CreateIfNotExists();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PriceContext, Configuration>());
        }
        public PriceContext(string conn = "PriceConnection") : base($"name={conn}")
        {
            Database.Log += s => System.Diagnostics.Debug.WriteLine(s);
        }

        public virtual DbSet<SearchItemEntity> SearchItems { get; set; }
        public virtual DbSet<InternetContentEntity> InternetContents { get; set; }
        public virtual DbSet<ContentEntity> Contents { get; set; }

        public virtual DbSet<ScheduleEntity> Schedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            const string prefix = "";
            modelBuilder.Configurations.Add(new SearchItemDbMap($"{prefix}{nameof(SearchItems)}"));
            modelBuilder.Configurations.Add(new InternetContentDbMap($"{prefix}history"));
            modelBuilder.Configurations.Add(new ContentDbMap($"{prefix}{nameof(Contents)}"));
            modelBuilder.Configurations.Add(new ScheduleDbMap($"{prefix}{nameof(Schedules)}"));
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

    }
}
