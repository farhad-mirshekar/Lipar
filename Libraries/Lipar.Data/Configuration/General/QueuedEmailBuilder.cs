using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class QueuedEmailBuilder : IEntityTypeConfiguration<QueuedEmail>
    {
        public void Configure(EntityTypeBuilder<QueuedEmail> builder)
        {
            builder.ToTable("QueuedEmails", "General");

            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd();

            builder.Property(q => q.Subject).HasMaxLength(1000);
            builder.Property(q => q.To).HasMaxLength(500);
            builder.Property(q => q.From).HasMaxLength(500);
            builder.Property(q => q.FromName).HasMaxLength(1000);
            builder.Property(q => q.ToName).HasMaxLength(1000);

            builder.HasOne(q => q.EmailAccount)
                .WithMany(e => e.QueuedEmails)
                .HasForeignKey(q => q.EmailAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
