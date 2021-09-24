using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Application
{
    public class AttributeControlTypeBuider : IEntityTypeConfiguration<AttributeControlType>
    {
        public void Configure(EntityTypeBuilder<AttributeControlType> builder)
        {
            builder.ToTable("AttributeControlTypes", schema: "Application");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Title).HasMaxLength(255).IsRequired();
        }
    }
}
