using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class OrderDetailBuilder : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails", "Application");

            builder.HasKey(od => od.Id);
            builder.Property(od => od.Id).ValueGeneratedOnAdd();
            builder.Property(od => od.ShippingCostName).HasMaxLength(3000);
            builder.Property(od => od.DeliveryDateName).HasMaxLength(3000);
            builder.Property(od => od.ProductCategoryName).HasMaxLength(3000);
            builder.Property(od => od.ProductPrice).HasColumnType("DECIMAL(18,3)");
            builder.Property(od => od.ProductDiscountPrice).HasColumnType("DECIMAL(18,3)");

            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
