using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Price.Db.Entities.Entities;

namespace Price.Db.MysSql.Mappings
{
    public class InternetContentMySqlMap : EntityTypeConfiguration<InternetContentEntity>
    {
        public InternetContentMySqlMap(string tableName)
        {
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasColumnName(nameof(InternetContentEntity.Id).ToLower())
                ;
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.spgz_Id)
                .IsOptional()
                ;

            Property(e => e.dt)
                ;

            Property(e => e.price)
                .IsOptional()
                ;

            Property(e => e.url)
                .HasColumnType("text")
                ;

            Property(e => e.src_id)
                .IsOptional()
                ;

            Property(e => e.contact_url)
                .IsOptional()
                .HasColumnType("tinytext")
                ;

            Property(e => e.task_id)
                .IsOptional()
                ;

            Property(e => e.session_id)
                .IsOptional()
                .HasMaxLength(128)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(InternetContentEntity.session_id)}", 1) { IsUnique = false }))
                ;

            Property(e => e.preview)
                .IsOptional()
                .HasColumnType("longtext")
                ;

            Property(e => e.selected)
                .IsOptional()
                ;

            Property(e => e.screenshot)
                .IsOptional()
                .HasColumnType("longblob")
                ;

            Property(e => e.currency)
                .IsOptional()
                .HasMaxLength(10)
                ;

            Property(e => e.opt)
                .IsOptional()
                ;

            Property(e => e.referer)
                .IsOptional()
                .HasColumnType("text")
                ;

            Property(e => e.prices)
                .IsOptional()
                .HasMaxLength(2048)
                ;

            Property(e => e.unit_price)
                .IsOptional()
                ;

            Property(e => e.unit)
                .IsOptional()
                .HasMaxLength(50)
                ;

            Property(e => e.weight)
                .IsOptional()
                .HasMaxLength(50)
                ;

            Property(e => e.rate)
                .IsOptional()
                .HasMaxLength(255)
                ;

            Property(e => e.PriceStatus)
                .IsOptional()
                ;

            ToTable($"{tableName}");

        }
    }
}