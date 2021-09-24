using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class StaticPageBuilder : IEntityTypeConfiguration<StaticPage>
    {
        public void Configure(EntityTypeBuilder<StaticPage> builder)
        {
            builder.ToTable("StaticPages", schema: "Portal");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Name).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(s => s.Title).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(s=>s.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(s => s.MetaKeywords).HasColumnType("NVARCHAR(MAX)");
            builder.Property(s => s.MetaDescription).HasColumnType("NVARCHAR(MAX)");

            builder.Property(s => s.IncludeInTopMenu).HasDefaultValue(false);

            builder.HasOne(s => s.ViewStatus)
                .WithMany(v => v.StaticPages)
                .HasForeignKey(s => s.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.User)
                .WithMany(u => u.StaticPages)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Remover)
                .WithMany(u => u.RemoverStaticPages)
                .HasForeignKey(s => s.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
