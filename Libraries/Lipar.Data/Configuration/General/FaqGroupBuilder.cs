using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class FaqGroupBuilder : IEntityTypeConfiguration<FaqGroup>
    {
        public void Configure(EntityTypeBuilder<FaqGroup> builder)
        {
            builder.ToTable("FaqGroups", schema: "General");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Name).HasMaxLength(3000).IsRequired();

            builder.HasOne(f => f.User)
                .WithMany(u => u.FaqGroups)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.Remover)
                .WithMany(u => u.RemoverFaqGroups)
                .HasForeignKey(f => f.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
