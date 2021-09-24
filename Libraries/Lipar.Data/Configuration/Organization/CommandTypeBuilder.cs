using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class CommandTypeBuilder : IEntityTypeConfiguration<CommandType>
    {
        public void Configure(EntityTypeBuilder<CommandType> builder)
        {
            builder.ToTable("CommandTypes", schema: "Organization");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasMaxLength(255).IsRequired();
        }
    }
}
