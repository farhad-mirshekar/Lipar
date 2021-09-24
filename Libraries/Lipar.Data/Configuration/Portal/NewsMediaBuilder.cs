using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    class NewsMediaBuilder : IEntityTypeConfiguration<NewsMedia>
    {
        public void Configure(EntityTypeBuilder<NewsMedia> builder)
        {
            builder.ToTable("News_Media_Mapping", schema: "Portal");

            builder.HasKey(bm => new { bm.NewsId, bm.MediaId });
        }
    }
}
