using Lipar.Entities.Domain.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Financial
{
    public class BankPortBuilder : IEntityTypeConfiguration<BankPort>
    {
        public void Configure(EntityTypeBuilder<BankPort> builder)
        {
            builder.ToTable("BankPorts", "Financial");

            builder.HasKey(bp => bp.Id);
            builder.Property(bp => bp.Id).ValueGeneratedOnAdd();

            builder.Property(bp => bp.Username).HasMaxLength(500);
            builder.Property(bp => bp.Password).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(bp => bp.EnabledType)
                .WithMany(e => e.BankPorts)
                .HasForeignKey(bp => bp.EnabledTypeId);

            builder.HasOne(bp => bp.Bank)
                .WithMany(b => b.BankPorts)
                .HasForeignKey(bp => bp.BankId);
        }
    }
}
