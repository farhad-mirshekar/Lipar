using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class ProductTagBuilder : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTags", schema: "Application");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name).HasMaxLength(400).IsRequired();
        }
    }
}
