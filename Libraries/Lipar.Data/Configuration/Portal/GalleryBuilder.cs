using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class GalleryBuilder : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder.ToTable("Galleries", schema: "Portal");

            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.Property(g => g.Name).HasMaxLength(3000).IsRequired();
            builder.Property(g => g.Name).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(g => g.VisitedCount).HasDefaultValue(0);

            builder.HasOne(g => g.User)
                .WithMany(u=>u.Galleries)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(g => g.Remover)
               .WithMany(u => u.RemoverGalleries)
               .HasForeignKey(g => g.RemoverId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(g => g.ViewStatus)
                .WithMany(v => v.Galleries)
                .HasForeignKey(g => g.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
