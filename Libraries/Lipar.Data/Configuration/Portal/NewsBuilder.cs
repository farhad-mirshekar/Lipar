using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class NewsBuilder : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News", schema: "Portal");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).ValueGeneratedOnAdd();

            builder.Property(n => n.Name).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(n => n.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(n => n.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(n => n.VisitedCount).HasDefaultValue(0);

            builder.HasOne(n => n.Category)
                .WithMany(c=>c.News)
                .HasForeignKey(n => n.CategoryId);

            builder.HasOne(n => n.Language)
                .WithMany(l=>l.News)
                .HasForeignKey(n => n.LanguageId);

            builder.HasOne(n => n.ViewStatus)
                .WithMany(v => v.News)
                .HasForeignKey(n => n.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.CommentStatus)
                .WithMany(c => c.News)
                .HasForeignKey(n => n.CommentStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.User)
                .WithMany(u => u.News)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(n => n.Remover)
                .WithMany(u => u.RemoverNews)
                .HasForeignKey(n => n.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
