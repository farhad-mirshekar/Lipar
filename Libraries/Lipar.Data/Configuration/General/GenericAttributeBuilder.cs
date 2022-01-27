using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class GenericAttributeBuilder : IEntityTypeConfiguration<GenericAttribute>
    {
        public void Configure(EntityTypeBuilder<GenericAttribute> builder)
        {
            builder.ToTable("GenericAttributes", "General");

            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.Property(g => g.KeyGroup).HasMaxLength(500);
            builder.Property(g => g.Key).HasMaxLength(500);
        }
    }
}
