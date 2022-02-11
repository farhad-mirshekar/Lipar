using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class GalleryMediaMapping : IEntityTypeConfiguration<GalleryMedia>
    {
        public void Configure(EntityTypeBuilder<GalleryMedia> builder)
        {
            builder.ToTable("Gallery_Media_Mapping", schema: "Portal");

            builder.HasKey(gm => gm.Id);
            builder.Property(gm => gm.Id).ValueGeneratedOnAdd();

            builder.HasOne(gm => gm.Gallery)
                .WithMany(g => g.GalleryMedias)
                .HasForeignKey(gm => gm.GalleryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(gm => gm.Media)
                .WithMany(m => m.GalleryMedias)
                .HasForeignKey(gm => gm.MediaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
