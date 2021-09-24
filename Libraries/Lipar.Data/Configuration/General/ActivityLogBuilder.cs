using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class ActivityLogBuilder : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs", schema: "General");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.EntityName).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.Comment).HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.ActivityLogType)
                .WithMany()
                .HasForeignKey(a => a.ActivityLogTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
