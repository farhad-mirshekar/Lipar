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

            builder.Property(bm => bm.BlogId).IsRequired();

            builder.Property(bm => bm.MediaId).IsRequired();
        }
    }
}
