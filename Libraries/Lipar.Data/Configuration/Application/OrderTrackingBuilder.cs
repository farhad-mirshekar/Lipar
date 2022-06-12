using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class OrderTrackingBuilder : IEntityTypeConfiguration<OrderTracking>
    {
        public void Configure(EntityTypeBuilder<OrderTracking> builder)
        {
            builder.ToTable("OrderTrackings", "Application");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderNumber).HasMaxLength(500);

            builder.HasOne(ot => ot.Order)
                   .WithMany(o => o.OrderTrackings)
                   .HasForeignKey(ot => ot.OrderId);
        }
    }
}
