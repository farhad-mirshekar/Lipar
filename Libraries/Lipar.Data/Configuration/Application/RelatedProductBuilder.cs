using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class RelatedProductBuilder : IEntityTypeConfiguration<RelatedProduct>
    {
        public void Configure(EntityTypeBuilder<RelatedProduct> builder)
        {
            builder.ToTable("RelatedProducts", schema : "Application");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.ProductId1).IsRequired();
            builder.Property(r => r.ProductId2).IsRequired();

            builder.Property(r => r.Priority).IsRequired();
        }
    }
}
