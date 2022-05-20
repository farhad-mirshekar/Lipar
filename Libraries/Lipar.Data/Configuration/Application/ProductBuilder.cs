using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductBuilder : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", schema: "Application");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).HasColumnType("DECIMAL(18,3)").IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Height).HasColumnType("DECIMAL(18,3)").IsRequired();
            builder.Property(p => p.Weight).HasColumnType("DECIMAL(18,3)").IsRequired();
            builder.Property(p => p.Width).HasColumnType("DECIMAL(18,3)").IsRequired();
            builder.Property(p => p.Length).HasColumnType("DECIMAL(18,3)").IsRequired();
            builder.Property(p => p.StockQuantity).IsRequired();

            builder.Property(p => p.MetaDescription);
            builder.Property(p => p.MetaKeywords).HasMaxLength(1000);
            builder.Property(p => p.MetaTitle).HasMaxLength(1000);
            builder.Property(p => p.Discount).HasColumnType("DECIMAL(18,3)");

            builder.HasOne(p => p.DiscountType)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.DiscountTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.DeliveryDate)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.DeliveryDateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.ShippingCost)
                 .WithMany(sh => sh.Products)
                 .HasForeignKey(p => p.ShippingCostId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
