using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Price.Db.Entities.Entities;

namespace Price.Db.MysSql.Mappings
{
    public class ContentMySqlMap : EntityTypeConfiguration<ContentEntity>
    {
        public ContentMySqlMap(string tableName)
        {
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Name)
                .IsOptional()
                ;

            Property(e => e.Price)
                .HasMaxLength(128)
                ;

            Property(e => e.Uri)
                .IsOptional()
                ;

            Property(e => e.CollectedAt)
                .IsOptional()
                ;

            Property(e => e.ElasticId)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(ContentEntity.ElasticId)}", 1) { IsUnique = false }))
                .HasMaxLength(128)
                ;

            Property(e => e.Okpd2)
                .IsOptional()
                .HasMaxLength(128)
                ;

            Property(e => e.SearchItemId)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(ContentEntity.SearchItemId)}", 1) { IsUnique = false }))
                .HasMaxLength(128)
                ;

            Property(e => e.Screenshot)
                .IsOptional()
                .HasMaxLength(128)
                ;

            ToTable($"{tableName}");

        }
    }
}