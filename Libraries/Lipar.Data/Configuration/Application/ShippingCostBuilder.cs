using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ShippingCostBuilder : IEntityTypeConfiguration<ShippingCost>
    {
        public void Configure(EntityTypeBuilder<ShippingCost> builder)
        {
            builder.ToTable("ShippingCosts", schema: "Application");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Name).HasMaxLength(3000).IsRequired();
            builder.Property(s => s.Priority).IsRequired();
            builder.Property(s => s.Price).HasColumnType("DECIMAL(18,3)").IsRequired();

            builder.Property(s => s.Description).HasMaxLength(3000);

            builder.HasOne(s => s.EnabledType)
                .WithMany(e => e.shippingCosts)
                .HasForeignKey(s => s.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.User)
                .WithMany(u => u.shippingCosts)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
