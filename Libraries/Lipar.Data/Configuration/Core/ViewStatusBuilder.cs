using Lipar.Entities.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Core
{
    public class ViewStatusBuilder : IEntityTypeConfiguration<ViewStatus>
    {
        public void Configure(EntityTypeBuilder<ViewStatus> builder)
        {
            builder.ToTable("ViewStatuses", schema: "Core");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();

            builder.Property(v => v.Title).HasMaxLength(255).IsRequired();
        }
    }
}
