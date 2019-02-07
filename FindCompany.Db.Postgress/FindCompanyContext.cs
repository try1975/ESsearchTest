using System.Data.Entity;
using FindCompany.Db.Entities.Entities;
using FindCompany.Db.Postgress.Mappings;


namespace FindCompany.Db.Postgress
{
    public class FindCompanyContext : DbContext
    {
        public FindCompanyContext() : base("name=FindCompanyConnection")
        {
            //Database.CreateIfNotExists();
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<FindCompanyContext, Configuration>());
        }
        public FindCompanyContext(string conn = "FindCompanyConnection") : base($"name={conn}")
        {
            Database.Log += s => System.Diagnostics.Debug.WriteLine(s);
        }

        public virtual DbSet<FindCompanyEntity> FindCompanies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            const string prefix = "";
            modelBuilder.Configurations.Add(new FindCompanyDbMap($"{prefix}find_company"));
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

    }
}
