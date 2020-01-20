using Price.Db.Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Price.Db.Postgress.Mappings
{
    public class ScheduleDbMap : EntityTypeConfiguration<ScheduleEntity>
    {
        public ScheduleDbMap(string tableName)
        {
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable($"{tableName}");
        }
    }
}
