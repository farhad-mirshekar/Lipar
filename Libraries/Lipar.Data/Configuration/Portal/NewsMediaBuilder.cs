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

            builder.HasKey(nm => nm.Id);
            builder.Property(nm => nm.Id).ValueGeneratedOnAdd();

            builder.HasOne(nm => nm.News)
                .WithMany(n => n.NewsMedias)
                .HasForeignKey(nm => nm.NewsId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(nm => nm.News)
                .WithMany(m => m.NewsMedias)
                .HasForeignKey(nm => nm.MediaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
