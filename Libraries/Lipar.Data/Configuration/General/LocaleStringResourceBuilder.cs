using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class LocaleStringResourceBuilder : IEntityTypeConfiguration<LocaleStringResource>
    {
        public void Configure(EntityTypeBuilder<LocaleStringResource> builder)
        {
            builder.ToTable("LocaleStringResources", schema: "General");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.ResourceName).HasColumnType("NVARCHAR(MAX)");
            builder.Property(l => l.ResourceValue).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(l => l.Language)
                .WithMany(l => l.LocaleStringResources)
                .HasForeignKey(l => l.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(l => l.User)
             .WithMany(u => u.LocaleStringResources)
             .HasForeignKey(l => l.UserId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
