using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class BlogCommentBuilder : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.ToTable("BlogComments", "Portal");

            builder.HasKey(bc => bc.Id);
            builder.Property(bc => bc.Id).ValueGeneratedOnAdd();

            builder.Property(bc => bc.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(bc => bc.UserId).IsRequired();

            builder.HasOne(bc => bc.Blog)
                .WithMany(b => b.BlogComments)
                .HasForeignKey(bc => bc.BlogId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bc => bc.Parent)
                .WithMany(bc => bc.Children)
                .HasForeignKey(bc => bc.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bc => bc.CommentStatus)
                    .WithMany(c => c.BlogComments)
                    .HasForeignKey(bc => bc.CommentStatusId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bc => bc.User)
                .WithMany(u => u.BlogComments)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(bc => bc.Remover)
                .WithMany(u => u.RemoverBlogComments)
                .HasForeignKey(bc => bc.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
