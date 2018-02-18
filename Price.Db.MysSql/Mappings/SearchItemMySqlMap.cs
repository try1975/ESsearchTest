﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Price.Db.Entities.Entities;

namespace Price.Db.MysSql.Mappings
{
    public class SearchItemMySqlMap : EntityTypeConfiguration<SearchItemEntity>
    {
        public SearchItemMySqlMap(string tableName)
        {
            HasKey(e => e.Id);
            //Property(e => e.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(SearchItemEntity.Name)}", 1) { IsUnique = false }))
                ;

            Property(e => e.InternetSessionId)
                .IsOptional()
                .HasMaxLength(128)
                ;

            Property(e => e.StartProcessed)
                .IsOptional()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(SearchItemEntity.StartProcessed)}", 1) { IsUnique = false }))
                ;

            Property(e => e.Source)
                .IsOptional()
                .HasMaxLength(128)
                ;

            Property(e => e.ExtId)
                .IsOptional()
                .HasMaxLength(128)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IX_{nameof(SearchItemEntity.ExtId)}", 1) { IsUnique = false }))
                ;

            Property(e => e.Normalizer)
                .IsOptional()
                ;

            Property(e => e.JsonText)
                .IsOptional()
                ;

            ToTable($"{tableName}");
        }
    }
}