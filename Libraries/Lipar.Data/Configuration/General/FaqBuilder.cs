using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class FaqBuilder : IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.ToTable("Faqs", schema: "General");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.UserId).IsRequired();
            builder.Property(f => f.Question).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(f => f.Answer).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.HasOne(f => f.FaqGroup)
                .WithMany(f => f.Faqs)
                .HasForeignKey(f => f.FaqGroupId);

            builder.HasOne(f => f.User)
                .WithMany(u => u.Faqs)
                .HasForeignKey(f => f.UserId);

        }
    }
}
