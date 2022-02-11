using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class UserAddressBuilder : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses", schema: "Organization");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.PostalCode).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Address).IsRequired();

            builder.HasOne(ua => ua.User)
                .WithMany(u=>u.UserAddresses)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.Country)
                .WithMany(c => c.UserAddresses)
                .HasForeignKey(ua => ua.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.Province)
                .WithMany(c => c.UserAddresses)
                .HasForeignKey(ua => ua.ProvinceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.City)
                .WithMany(c => c.UserAddresses)
                .HasForeignKey(ua => ua.CityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
