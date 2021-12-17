using Lipar.Entities.Domain.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Financial
{
    public class BankBuilder : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Banks", "Financial");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Name).HasMaxLength(500).IsRequired();
            builder.Property(b => b.PaymentUri).HasMaxLength(500).IsRequired();
            builder.Property(b => b.RedirectUri).HasMaxLength(500).IsRequired();

            builder.HasOne(b => b.EnabledType)
                .WithMany(e => e.Banks)
                .HasForeignKey(b => b.EnabledTypeId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(b => b.User)
                .WithMany(u => u.Banks)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
