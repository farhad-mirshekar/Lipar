using Lipar.Entities.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lipar.Data.Configuration.General
{
    public class ProvinceBuilder : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Provinces", "General");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasMaxLength(500);

            builder.HasOne(p => p.Country)
                   .WithMany(c => c.Provinces)
                   .HasForeignKey(p => p.CountryId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
