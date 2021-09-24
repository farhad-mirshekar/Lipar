using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class ContactUsTypeBuilder : IEntityTypeConfiguration<ContactUsType>
    {
        public void Configure(EntityTypeBuilder<ContactUsType> builder)
        {
            builder.ToTable("ContactUsTypes", schema: "General");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasMaxLength(255).IsRequired();
        }
    }
}
