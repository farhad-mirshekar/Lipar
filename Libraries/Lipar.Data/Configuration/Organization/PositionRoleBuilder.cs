using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class PositionRoleBuilder : IEntityTypeConfiguration<PositionRole>
    {
        public void Configure(EntityTypeBuilder<PositionRole> builder)
        {
            builder.ToTable("PositionRoles", schema: "Organization");

            builder.HasKey(pr => pr.Id);
            builder.Property(pr => pr.Id).ValueGeneratedOnAdd();

            builder.Property(pr => pr.RoleId).IsRequired();
            builder.Property(pr => pr.PositionId).IsRequired();

        }
    }
}
