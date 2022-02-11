using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductAttributeValueBuilder : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.ToTable("ProductAttributeValues", schema: "Application");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasColumnType("NVARCHAR(1000)").IsRequired();

            builder.Property(p => p.Price).HasColumnType("DECIMAL(18,3)");

            builder.HasOne(p => p.ProductAttributeMapping)
                .WithMany(pam => pam.ProductAttributeValues)
                .HasForeignKey(p => p.ProductAttributeMappingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
