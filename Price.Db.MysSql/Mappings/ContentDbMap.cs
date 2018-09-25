using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Price.Db.Entities.Entities;

namespace Price.Db.Postgress.Mappings
{
    public class ContentDbMap : EntityTypeConfiguration<ContentEntity>
    {
        public ContentDbMap(string tableName)
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
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(ContentEntity.Screenshot)}", 1) { IsUnique = false }))
                ;

            Property(e => e.PriceStatus)
                .IsOptional()
                ;

            Property(e => e.RejectReason)
                .IsOptional()
                .HasMaxLength(128)
                ;

            Property(e => e.ManualPrice)
                .IsOptional()
                ;

            Property(e => e.ProdStatus)
                .IsOptional()
                .HasColumnName("ProdStatus")
                ;

            ToTable($"{tableName}");

        }
    }
}