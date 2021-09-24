using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Lipar.Data.Configuration.Application
{
    public class ProductCommentBuilder : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable("ProductComments", schema: "Application");

            builder.HasKey(pc => pc.Id);
            builder.Property(pc => pc.Id).ValueGeneratedOnAdd();

            builder.Property(pc => pc.CommentText).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(pc => pc.ReplyText).HasColumnType("NVARCHAR(MAX)");

            builder.Property(pc => pc.CreationDate).HasDefaultValue(DateTime.Now);

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductComments)
                .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(pc => pc.User)
                .WithMany(u => u.ProductComments)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pc => pc.Parent)
                .WithMany(pc => pc.Children)
                .HasForeignKey(pc => pc.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pc => pc.CommentStatus)
                .WithMany(c => c.ProductComments)
                .HasForeignKey(pc => pc.CommentStatusId);

        }
    }
}
