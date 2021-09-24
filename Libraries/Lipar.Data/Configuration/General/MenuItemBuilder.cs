using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class MenuItemBuilder : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems", schema: "General");

            builder.HasKey(mi => mi.Id);
            builder.Property(mi => mi.Id).ValueGeneratedOnAdd();

            builder.Property(mi => mi.Name).HasMaxLength(1000).IsRequired();

            builder.HasOne(mi => mi.Menu)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(mi => mi.MenuId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(mi => mi.Parent)
                .WithMany(mi => mi.Children)
                .HasForeignKey(mi => mi.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
