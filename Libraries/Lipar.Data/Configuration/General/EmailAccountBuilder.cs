using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class EmailAccountBuilder : IEntityTypeConfiguration<EmailAccount>
    {
        public void Configure(EntityTypeBuilder<EmailAccount> builder)
        {
            builder.ToTable("EmailAccounts", "General");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(255);
            builder.Property(e => e.Username).HasMaxLength(255);
            builder.Property(e => e.Password).HasMaxLength(255);
            builder.Property(e => e.Host).HasMaxLength(255);
            builder.Property(e => e.Email).HasMaxLength(255);
        }
    }
}
