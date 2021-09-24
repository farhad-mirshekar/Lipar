using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class CategoryBuilder : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", schema: "Application");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).HasMaxLength(3000).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(3000);
            builder.Property(c => c.MetaDescription).HasMaxLength(3000);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.EnabledType)
                .WithMany(e=>e.ApplicationCategories)
                .HasForeignKey(c => c.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.User)
                .WithMany(u => u.ApplicationCategories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(c => c.Remover)
                .WithMany(u => u.ApplicationRemoverCategories)
                .HasForeignKey(c => c.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
