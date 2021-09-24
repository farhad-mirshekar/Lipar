using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class CommandBuilder : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.ToTable("Commands", schema: "Organization");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(500);
            builder.Property(c => c.SystemName).IsRequired().HasMaxLength(500);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(c => c.Roles)
            //    .WithMany(r => r.Commands)
            //    .UsingEntity<RolePermission>
            //    (
            //        j => j.HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId).IsRequired(),
            //        j => j.HasOne(rp => rp.Command).WithMany().HasForeignKey(rp => rp.CommandId).IsRequired(),
            //        j =>
            //        {
            //            j.HasKey(rp => new { rp.RoleId, rp.CommandId });
            //            j.ToTable("RolePermission", schema : "Org");
            //        }
            //    );
        }
    }
}
