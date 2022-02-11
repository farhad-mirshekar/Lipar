using Lipar.Entities.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Organization
{
    public class UserPasswordBuilder : IEntityTypeConfiguration<UserPassword>
    {
        public void Configure(EntityTypeBuilder<UserPassword> builder)
        {
            builder.ToTable("UserPasswords", schema: "Organization");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Password).IsRequired().HasColumnType("nvarchar(max)");

            builder.HasOne(up => up.User)
                .WithMany(u => u.UserPasswords)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.PasswordFormatType)
                .WithMany(p => p.UserPasswords)
                .HasForeignKey(u => u.PasswordFormatTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
