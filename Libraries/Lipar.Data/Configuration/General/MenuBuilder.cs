using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class MenuBuilder : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus", schema: "General");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Name).HasMaxLength(3000).IsRequired();

            builder.HasOne(m => m.Language)
                .WithMany(l=>l.Menus)
                .HasForeignKey(m => m.LanguageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
