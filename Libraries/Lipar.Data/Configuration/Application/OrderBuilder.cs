using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class OrderBuilder : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Application");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.Price).HasColumnType("DECIMAL(18,3)");
            builder.Property(o => o.ShoppingCartRate).HasColumnType("DECIMAL(18,3)");

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.UserAddress)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.UserAddressId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
