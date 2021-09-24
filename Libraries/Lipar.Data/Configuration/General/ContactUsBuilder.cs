using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class ContactUsBuilder : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.ToTable("ContactUs", schema: "General");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.FirstName).HasMaxLength(200).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.HasOne(c => c.contactUsType)
                .WithMany(cu => cu.ContactUs)
                .HasForeignKey(c => c.ContactUsTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
