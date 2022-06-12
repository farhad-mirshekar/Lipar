using Lipar.Core.Common;
using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class OrderTrackingDocStateBuilder : IEntityTypeConfiguration<OrderTrackingDocState>
    {
        public void Configure(EntityTypeBuilder<OrderTrackingDocState> builder)
        {
            builder.ToTable("OrderTrackingDocStates","Application");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();

            builder.Property(o => o.Title).HasMaxLength(1000);

            builder.Property(o => o.CreationDate).HasDefaultValue(CommonHelper.GetCurrentDateTime());
        }
    }
}
