using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class OrderPaymentStatusBuilder : IEntityTypeConfiguration<OrderPaymentStatus>
    {
        public void Configure(EntityTypeBuilder<OrderPaymentStatus> builder)
        {
            builder.ToTable("OrderPaymentStatuses", "Application");

            builder.HasKey(o => o.Id);

            builder.HasOne(ops => ops.Order)
                .WithMany(o => o.OrderPaymentStatuses)
                .HasForeignKey(ops => ops.OrderId);


            builder.HasOne(ops => ops.shoppingCartItem)
                .WithMany(o => o.OrderPaymentStatuses)
                .HasForeignKey(ops => ops.ShoppingCartItemId);
        }
    }
}
