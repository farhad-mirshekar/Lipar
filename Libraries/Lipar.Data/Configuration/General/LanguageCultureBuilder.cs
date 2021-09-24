using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class LanguageCultureBuilder : IEntityTypeConfiguration<LanguageCulture>
    {
        public void Configure(EntityTypeBuilder<LanguageCulture> builder)
        {
            builder.ToTable("LanguageCultures", schema: "General");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.Title).HasMaxLength(255).IsRequired();
        }
    }
}
