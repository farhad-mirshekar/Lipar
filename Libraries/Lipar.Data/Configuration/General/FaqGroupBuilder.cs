using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class FaqGroupBuilder : IEntityTypeConfiguration<FaqGroup>
    {
        public void Configure(EntityTypeBuilder<FaqGroup> builder)
        {
            builder.ToTable("FaqGroups", schema: "General");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Name).HasMaxLength(3000).IsRequired();
        }
    }
}
