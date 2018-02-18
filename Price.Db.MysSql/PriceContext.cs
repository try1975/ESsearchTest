using System.Data.Entity;
using MySql.Data.Entity;
using Price.Db.Entities.Entities;
using Price.Db.MysSql.Mappings;
using Price.Db.MysSql.Migrations;

namespace Price.Db.MysSql
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class PriceContext : DbContext
    {
        public PriceContext() : base("name=PriceConnection")
        {
            //Database.CreateIfNotExists();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PriceContext, Configuration>());
        }
        public PriceContext(string conn = "PriceConnection") : base($"name={conn}")
        {
        }

        public virtual DbSet<PacketEntity> Packets { get; set; }
        public virtual DbSet<SearchItemEntity> SearchItems { get; set; }
        public virtual DbSet<InternetContentEntity> InternetContents { get; set; }
        public virtual DbSet<ContentEntity> Contents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            const string prefix = "";
            modelBuilder.Configurations.Add(new PacketMySqlMap($"{prefix}{nameof(Packets)}"));
            modelBuilder.Configurations.Add(new SearchItemMySqlMap($"{prefix}{nameof(SearchItems)}"));
            modelBuilder.Configurations.Add(new InternetContentMySqlMap($"{prefix}history"));
            modelBuilder.Configurations.Add(new ContentMySqlMap($"{prefix}{nameof(Contents)}"));
        }

    }
}
