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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.FullName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Email)
               .HasMaxLength(200)
               .IsRequired();

            builder.Property(t => t.Phone)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(t => t.Gender)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(t => t.DateOfBirth)
                   .IsRequired();

            builder.Property(t => t.Address)
               .HasMaxLength(200)
               .IsRequired();

            builder.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
