using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class DynamicPageBuilder : IEntityTypeConfiguration<DynamicPage>
    {
        public void Configure(EntityTypeBuilder<DynamicPage> builder)
        {
            builder.ToTable("DynamicPages", schema: "Portal");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();

            builder.Property(d => d.Name).HasColumnType("NVARCHAR(1000)").IsRequired();
            builder.Property(d => d.Title).HasColumnType("NVARCHAR(1000)").IsRequired();
            builder.Property(d => d.Description).HasColumnType("NVARCHAR(MAX)");
            builder.Property(d => d.IncludeInTopMenu).HasDefaultValue(false);

            builder.HasOne(d => d.ViewStatus)
                .WithMany(v => v.DynamicPages)
                .HasForeignKey(d => d.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.User)
                .WithMany(u => u.DynamicPages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Remover)
                .WithMany(u => u.RemoverDynamicPages)
                .HasForeignKey(d => d.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
