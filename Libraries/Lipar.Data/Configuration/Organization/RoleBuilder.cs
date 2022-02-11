using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class RoleBuilder : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", schema: "Organization");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.Name).HasMaxLength(1000).IsRequired();

            builder.HasOne(r => r.Center)
                .WithMany(c => c.Roles)
                .HasForeignKey(r => r.CenterId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
