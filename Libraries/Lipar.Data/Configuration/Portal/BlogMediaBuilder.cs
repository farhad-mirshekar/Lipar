using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class BlogMediaBuilder : IEntityTypeConfiguration<BlogMedia>
    {
        public void Configure(EntityTypeBuilder<BlogMedia> builder)
        {
            builder.ToTable("Blog_Media_Mapping", schema: "Portal");

            builder.HasKey(bm => bm.Id);
            builder.Property(bm => bm.Id).ValueGeneratedOnAdd();

            builder.HasOne(bm => bm.Blog)
                .WithMany(b => b.BlogMedias)
                .HasForeignKey(bm => bm.BlogId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bm => bm.Media)
                .WithMany(b => b.BlogMedias)
                .HasForeignKey(bm => bm.MediaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
