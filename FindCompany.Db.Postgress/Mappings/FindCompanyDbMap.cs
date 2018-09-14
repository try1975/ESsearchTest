using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FindCompany.Db.Entities.Entities;

namespace FindCompany.Db.Postgress.Mappings
{
    public class FindCompanyDbMap : EntityTypeConfiguration<FindCompanyEntity>
    {
        public FindCompanyDbMap(string tableName)
        {
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                ;

            Property(e => e.Host)
                .HasColumnName("host")
                ;

            Property(e => e.Inn)
                .HasColumnName("inn")
                ;

            Property(e => e.Name)
                .HasColumnName("name")
                ;

            ToTable($"{tableName}");

        }
    }
}