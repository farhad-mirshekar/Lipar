using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class PasswordFormatTypeBuilder : IEntityTypeConfiguration<PasswordFormatType>
    {
        public void Configure(EntityTypeBuilder<PasswordFormatType> builder)
        {
            builder.ToTable("PasswordFormatTypes", schema: "Organization");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Title).HasMaxLength(255).IsRequired();
        }
    }
}
