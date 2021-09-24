
using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class RolePermissionBuilder : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions", schema: "Organization");

            builder.HasKey(pr => pr.Id);
            builder.Property(pr => pr.Id).ValueGeneratedOnAdd();

            builder.Property(pr => pr.RoleId).IsRequired();
            builder.Property(pr => pr.CommandId).IsRequired();

            builder.HasOne(pr => pr.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(pr => pr.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pr => pr.Command)
                .WithMany(c => c.RolePermissions)
                .HasForeignKey(pr => pr.CommandId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
