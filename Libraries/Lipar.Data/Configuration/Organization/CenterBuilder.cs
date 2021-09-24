using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lipar.Entities.Domain.Organization;

namespace Lipar.Data.Configuration.Organization
{
    public class CenterBuilder : IEntityTypeConfiguration<Center>
    {
        public void Configure(EntityTypeBuilder<Center> builder)
        {
            builder.ToTable("Centers", schema: "Organization");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Code).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(256);

            builder.Property(a => a.Comment);

            builder.HasOne(c => c.EnabledType)
                .WithMany(e => e.Centers)
                .HasForeignKey(e => e.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
