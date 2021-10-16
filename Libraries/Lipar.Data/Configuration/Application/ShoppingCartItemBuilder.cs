using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ShoppingCartItemBuilder : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.ToTable("ShoppingCartItems", schema: "Application");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Quantity).HasDefaultValue(1).IsRequired();
            builder.Property(s => s.ShoppingCartItemId).IsRequired();

            builder.HasOne(s => s.Product)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.User)
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
