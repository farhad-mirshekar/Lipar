using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class LanguageBuilder : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages", schema: "General");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.Name).HasMaxLength(1000).IsRequired();

            builder.HasOne(l => l.ViewStatus)
                .WithMany(v => v.Languages)
                .HasForeignKey(l => l.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(l => l.LanguageCulture)
                .WithMany(lc => lc.Languages)
                .HasForeignKey(l => l.LanguageCultureId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
