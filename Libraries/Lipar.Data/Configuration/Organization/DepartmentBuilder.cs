using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class DepartmentBuilder : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments", schema: "Organization");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(d => d.Name).HasMaxLength(1000).IsRequired();

            builder.HasOne(d => d.Center)
                .WithMany(c=>c.Departments)
                .HasForeignKey(d=>d.CenterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Parent)
               .WithMany(d => d.Children)
               .HasForeignKey(d => d.ParentId)
               .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(d => d.DepartmentType)
                .WithMany(dt => dt.Departments)
                .HasForeignKey(d => d.DepartmentTypeId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(d => d.EnabledType)
                .WithMany(e => e.Departments)
                .HasForeignKey(u => u.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
