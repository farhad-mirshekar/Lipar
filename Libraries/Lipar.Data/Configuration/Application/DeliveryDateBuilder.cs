using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class DeliveryDateBuilder : IEntityTypeConfiguration<DeliveryDate>
    {
        public void Configure(EntityTypeBuilder<DeliveryDate> builder)
        {
            builder.ToTable("DeliveryDates", schema: "Application");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Name).HasMaxLength(3000).IsRequired();
            builder.Property(d => d.Priority).IsRequired();

            builder.Property(d => d.Description).HasMaxLength(3000);

            builder.HasOne(d => d.EnabledType)
                .WithMany(e => e.DeliveryDates)
                .HasForeignKey(d => d.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
