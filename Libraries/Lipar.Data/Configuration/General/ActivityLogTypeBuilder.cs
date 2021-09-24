﻿using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lipar.Data.Configuration.General
{
    public class ActivityLogTypeBuilder : IEntityTypeConfiguration<ActivityLogType>
    {
        public void Configure(EntityTypeBuilder<ActivityLogType> builder)
        {
            builder.ToTable("ActivityLogTypes", "General");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.SystemKeyword).HasMaxLength(200).IsRequired();
            builder.Property(a => a.SystemKeyword).HasMaxLength(200).IsRequired();

            builder.HasOne(a => a.ViewStatus)
                .WithMany(v => v.ActivityLogTypes)
                .HasForeignKey(a => a.ViewStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
