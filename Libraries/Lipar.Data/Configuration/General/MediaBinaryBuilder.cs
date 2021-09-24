using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class MediaBinaryBuilder : IEntityTypeConfiguration<MediaBinary>
    {
        public void Configure(EntityTypeBuilder<MediaBinary> builder)
        {
            builder.ToTable("MediaBinaries", schema: "General");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.BinaryData).IsRequired();

            builder.HasOne(m => m.Media)
                .WithOne()
                .IsRequired()
                .HasForeignKey<MediaBinary>(m => m.MediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
