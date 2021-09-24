using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class NewsCommentBuilder : IEntityTypeConfiguration<NewsComment>
    {
        public void Configure(EntityTypeBuilder<NewsComment> builder)
        {
            builder.ToTable("NewsComments", "Portal");

            builder.HasKey(bc => bc.Id);
            builder.Property(bc => bc.Id).ValueGeneratedOnAdd();

            builder.Property(bc => bc.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(bc => bc.UserId).IsRequired();

            builder.HasOne(bc => bc.News)
                .WithMany(b => b.NewsComments)
                .HasForeignKey(bc => bc.NewsId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bc => bc.Parent)
                .WithMany(bc => bc.Children)
                .HasForeignKey(bc => bc.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bc => bc.CommentStatus)
                    .WithMany(c => c.NewsComments)
                    .HasForeignKey(bc => bc.CommentStatusId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bc => bc.User)
                .WithMany(u => u.NewsComments)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(bc => bc.Remover)
                .WithMany(u => u.RemoverNewsComments)
                .HasForeignKey(bc => bc.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
