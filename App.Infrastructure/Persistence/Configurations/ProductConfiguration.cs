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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                    .HasMaxLength(220)
                    .IsRequired();

            builder.Property(p => p.Description)
                    .HasMaxLength(500)
                    .IsRequired();

            builder.Property(p => p.Image)
                    .HasMaxLength(220);

            builder.Property(p => p.Quantity)
                    .IsRequired();

            builder.Property(p => p.SellingUnitPrice)
                    .IsRequired();

            builder.Property(p => p.UnitPrice)
                    .IsRequired();

            builder.HasMany(c => c.ProductCategories)
                    .WithOne(pc => pc.Product)
                    .HasForeignKey(pc => pc.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.OrderDetails)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
