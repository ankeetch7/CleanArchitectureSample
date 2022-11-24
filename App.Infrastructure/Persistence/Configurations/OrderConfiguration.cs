using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Amount)
                    .IsRequired();

            builder.Property(o => o.OrderAddress)
                    .HasMaxLength(220)
                    .IsRequired();

            builder.Property(o => o.OrderEmail)
                    .HasMaxLength(220)
                    .IsRequired();

            builder.Property(o => o.OrderPhone)
                    .HasMaxLength(20)
                    .IsRequired();

            builder.Property(o => o.OrderDate)
                    .IsRequired();

            builder.HasMany(c => c.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
