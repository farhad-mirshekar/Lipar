using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class UserBuilder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", schema: "Organization");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.UserGuid).IsRequired();

            builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.NationalCode).IsRequired().HasMaxLength(10);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(300);

            builder.HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.EnabledType)
                .WithMany(e => e.Users)
                .HasForeignKey(u => u.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Gender)
                    .WithMany(g => g.Users)
                    .HasForeignKey(u => u.GenderId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
