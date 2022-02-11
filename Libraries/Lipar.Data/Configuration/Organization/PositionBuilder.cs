using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class PositionBuilder : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Positions", schema: "Organization");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Positions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Center)
                .WithMany(c => c.Positions)
                .HasForeignKey(p => p.CenterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Department)
               .WithMany(d => d.Positions)
               .HasForeignKey(p => p.DepartmentId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.PositionType)
                .WithMany(pt => pt.Positions)
                .HasForeignKey(p => p.PositionTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
