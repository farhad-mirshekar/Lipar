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

            builder.Property(bm => bm.GalleryId).IsRequired();

            builder.Property(bm => bm.MediaId).IsRequired();
        }
    }
}
