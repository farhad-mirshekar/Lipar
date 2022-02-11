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

            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.Id).ValueGeneratedOnAdd();

            builder.HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMedias)
                .HasForeignKey(pm => pm.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pm => pm.Media)
                .WithMany(m => m.ProductMedias)
                .HasForeignKey(pm => pm.MediaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
