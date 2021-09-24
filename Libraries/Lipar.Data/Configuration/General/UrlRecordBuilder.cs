using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class UrlRecordBuilder : IEntityTypeConfiguration<UrlRecord>
    {
        public void Configure(EntityTypeBuilder<UrlRecord> builder)
        {
            builder.ToTable("UrlRecords", schema: "General");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Slug).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.HasOne(u => u.Language)
                .WithMany(l => l.UrlRecords)
                .HasForeignKey(u => u.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.EnabledType)
                .WithMany(e => e.UrlRecords)
                .HasForeignKey(u => u.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
