using Lipar.Entities.Domain.Portal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.Portal
{
    public class DynamicPageDetailBuilder : IEntityTypeConfiguration<DynamicPageDetail>
    {
        public void Configure(EntityTypeBuilder<DynamicPageDetail> builder)
        {
            builder.ToTable("DynamicPageDetails", schema: "Portal");

            builder.HasKey(dd => dd.Id);
            builder.Property(dd => dd.Id).ValueGeneratedOnAdd();

            builder.Property(dd => dd.Name).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(dd => dd.Title).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(dd => dd.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(dd => dd.Body).HasColumnType("NVARCHAR(MAX)").IsRequired();

            builder.Property(dd => dd.MetaKeywords).HasColumnType("NVARCHAR(MAX)");
            builder.Property(dd => dd.MetaDescription).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(dd => dd.DynamicPage)
                .WithMany(d => d.DynamicPageDetails)
                .HasForeignKey(dd => dd.DynamicPageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(dp => dp.ViewStatus)
                .WithMany(v => v.DynamicPageDetails)
                .HasForeignKey(dp => dp.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(dd => dd.User)
                .WithMany(u => u.DynamicPageDetails)
                .HasForeignKey(dd => dd.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(dd => dd.Remover)
                .WithMany(u => u.RemoverDynamicPageDetails)
                .HasForeignKey(dd => dd.RemoverId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
