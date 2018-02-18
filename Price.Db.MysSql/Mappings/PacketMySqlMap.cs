using System.Data.Entity.ModelConfiguration;
using Price.Db.Entities.Entities;

namespace Price.Db.MysSql.Mappings
{
    public class PacketMySqlMap : EntityTypeConfiguration<PacketEntity>
    {
        public PacketMySqlMap(string tableName)
        {
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasMaxLength(50)
                ;

            ToTable($"{tableName}");
        }
    }
}