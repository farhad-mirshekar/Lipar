using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductAttributeMappingBuilder : IEntityTypeConfiguration<ProductAttributeMapping>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeMapping> builder)
        {
            builder.ToTable("Product_ProductAttribute_Mapping", schema: "Application");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(pa => pa.Product)
                .WithMany(p => p.ProductAttributeMappings)
                .HasForeignKey(pa => pa.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.ProductAttribute)
                .WithMany(pa => pa.ProductAttributeMappings)
                .HasForeignKey(p => p.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.AttributeControlType)
                .WithMany(a => a.ProductAttributeMappings)
                .HasForeignKey(p => p.AttributeControlTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
