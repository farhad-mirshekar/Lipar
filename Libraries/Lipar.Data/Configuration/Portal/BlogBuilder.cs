using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class BlogBuilder : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs", schema: "Portal");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Name).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(b => b.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(b => b.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(b => b.VisitedCount).HasDefaultValue(0);

            builder.HasOne(b => b.Category)
                .WithMany(c=>c.Blogs)
                .HasForeignKey(b => b.CategoryId);

            builder.HasOne(b => b.Language)
                .WithMany(l=>l.Blogs)
                .HasForeignKey(b => b.LanguageId);

            builder.HasOne(b => b.ViewStatus)
                .WithMany(v => v.Blogs)
                .HasForeignKey(b => b.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.CommentStatus)
               .WithMany(c => c.Blogs)
               .HasForeignKey(b => b.CommentStatusId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Remover)
                .WithMany(u => u.RemoverBlogs)
                .HasForeignKey(b => b.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
