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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                    .HasMaxLength(220)
                    .IsRequired();

            builder.Property(c => c.Description)
                    .HasMaxLength(500)
                    .IsRequired();

            builder.Property(c => c.Image)
                    .HasMaxLength(500)
                    .IsRequired();

            builder.HasMany(c => c.ProductCategories)
                    .WithOne(pc => pc.Category)
                    .HasForeignKey(pc => pc.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
