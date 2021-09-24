using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class UserAddressBuilder : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddress", schema: "Organization");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.PostalCode).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Address).HasColumnType("nvarchar(max)").IsRequired();

            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .IsRequired();
        }
    }
}
