using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public interface IApplicationDbContext
    {
         DbSet<Product> Products { get; set; }
         DbSet<Order> Orders { get; set; }
         DbSet<Category> Categories { get; set; }
         DbSet<ProductCategory> ProductCategories { get; set; }
         DbSet<OrderDetail> OrderDetails { get; set; }
         Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
