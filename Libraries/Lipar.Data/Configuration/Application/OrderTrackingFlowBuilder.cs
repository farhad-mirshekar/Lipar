using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class OrderTrackingFlowBuilder : IEntityTypeConfiguration<OrderTrackingFlow>
    {
        public void Configure(EntityTypeBuilder<OrderTrackingFlow> builder)
        {
            builder.ToTable("OrderTrackingFlows", "Application");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Description).HasMaxLength(5000);

            builder.HasOne(otf => otf.OrderTracking)
                   .WithMany(ot => ot.OrderTrackingFlows)
                   .HasForeignKey(otf => otf.OrderTrackingId);

            builder.HasOne(otf => otf.FromPosition)
                  .WithMany(p => p.FromOrderTrackingFlows)
                  .HasForeignKey(otf => otf.FromPositionId);

            builder.HasOne(otf => otf.ToPosition)
                  .WithMany(p => p.ToOrderTrackingFlows)
                  .HasForeignKey(otf => otf.ToPositionId);

            builder.HasOne(otf => otf.FromDocState)
                .WithMany(d => d.FromDocStates)
                .HasForeignKey(otf => otf.FromDocStateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(otf => otf.ToDocState)
                .WithMany(d => d.ToDocStates)
                .HasForeignKey(otf => otf.ToDocStateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
