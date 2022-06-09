using Lipar.Entities.Domain.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lipar.Data.Configuration.Application
{
    public class OrderDetailAttributeBuilder : IEntityTypeConfiguration<OrderDetailAttribute>
    {
        public void Configure(EntityTypeBuilder<OrderDetailAttribute> builder)
        {
            builder.ToTable("OrderDetailAttributes", "Application");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Price).HasColumnType("DECIMAL(18,3)");
            builder.Property(p => p.AttributeName).HasColumnType("NVARCHAR(1000)").IsRequired();

            builder.HasOne(o => o.OrderDetail)
                .WithMany(od => od.OrderDetailAttributes)
                .HasForeignKey(o => o.OrderDetailId);
        }
    }
}
