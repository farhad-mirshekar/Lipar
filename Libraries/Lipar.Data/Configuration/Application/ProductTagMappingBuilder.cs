using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductTagMappingBuilder : IEntityTypeConfiguration<ProductTagMapping>
    {
        public void Configure(EntityTypeBuilder<ProductTagMapping> builder)
        {
            builder.ToTable("Product_Tag_Mappings", schema: "Application");

            builder.HasKey(pt => pt.Id);

            builder.HasOne(ptm => ptm.ProductTag)
                .WithMany(pt => pt.ProductTagMappings)
                .HasForeignKey(ptm => ptm.ProductTagId);

            builder.HasOne(ptm => ptm.Product)
                .WithMany(pt => pt.ProductTagMappings)
                .HasForeignKey(ptm => ptm.ProductId);

        }
    }
}
