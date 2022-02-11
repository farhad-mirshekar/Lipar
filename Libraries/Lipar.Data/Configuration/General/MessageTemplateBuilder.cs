using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class MessageTemplateBuilder : IEntityTypeConfiguration<MessageTemplate>
    {
        public void Configure(EntityTypeBuilder<MessageTemplate> builder)
        {
            builder.ToTable("MessageTemplates", "General");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Name).HasMaxLength(500);
            builder.Property(m => m.Subject).HasMaxLength(1000);

            builder.HasOne(m => m.EmailAccount)
                .WithMany(e => e.MessageTemplates)
                .HasForeignKey(m => m.EmailAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
