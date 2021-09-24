using Lipar.Entities.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Core
{
    public class EnabledTypeBuilder : IEntityTypeConfiguration<EnabledType>
    {
        public void Configure(EntityTypeBuilder<EnabledType> builder)
        {
            builder.ToTable("EnabledTypes", schema: "Core");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Title).HasMaxLength(255).IsRequired();

            builder.Property(e => e.CreationDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
