using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductMediaBuilder : IEntityTypeConfiguration<ProductMedia>
    {
        public void Configure(EntityTypeBuilder<ProductMedia> builder)
        {
            builder.ToTable("Product_Media_Mapping", schema: "Application");

            builder.Property(pm => pm.ProductId).IsRequired();

            builder.Property(pm => pm.MediaId).IsRequired();
        }
    }
}
