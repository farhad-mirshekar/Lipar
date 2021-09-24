using Lipar.Entities.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Core
{
    public class CommentStatusBuilder : IEntityTypeConfiguration<CommentStatus>
    {
        public void Configure(EntityTypeBuilder<CommentStatus> builder)
        {
            builder.ToTable("CommentStatuses", schema: "Core");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasMaxLength(255).IsRequired();
        }
    }
}
