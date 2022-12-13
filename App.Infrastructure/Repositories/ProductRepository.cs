using App.Application.Command.Product.CreateProduct;
using App.Application.Command.Product.UpdateProduct;
using App.Application.Error;
using App.Application.Interfaces.IRepositories;
using App.Application.Services;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Extensions;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace App.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public ProductRepository(ApplicationDbContext context, ICurrentUserService currentUser) : base(context)
        {
            _context = context;
            _currentUser = currentUser;
        }
        
        public async  Task<IEnumerable<ProductVm>> GetAllProducts()
        {
            return await  _context.Products
                                    .OrderByDescending(x => x.CreatedDate)
                                    .Where(x => x.IsDeleted == false)
                                    .Select(x => new ProductVm
                                    {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Description = x.Description,
                                            UnitPrice = x.UnitPrice,
                                            SellingUnitPrice = x.SellingUnitPrice,
                                            ProductStatus = x.ProductStatus.GetEnumDisplayName(),
                                            Quantity = x.Quantity,
                                            CreatedBy = _context.Users.FirstOrDefault(y => y.Id == x.CreatedBy).FullName,
                                        }).ToListAsync();
                                    }

        public async Task CreateProduct(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                UnitPrice = command.UnitPrice,
                SellingUnitPrice = command.SellingUnitPrice,
                Quantity = command.Quantity,
                Image = command.Image
            };
            
            _context.Products.Add(product);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(UpdateProductCommand command)
        {
            var productDetails = await _context.Products.FindAsync(command.Id);
            productDetails.Name = command.Name;
            productDetails.Description = command.Description;
            productDetails.UnitPrice = command.UnitPrice;
            productDetails.SellingUnitPrice = command.SellingUnitPrice;
            productDetails.Quantity = command.Quantity;
            productDetails.Image = command.Image;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid id)
        {
            var productDetails = await _context.Products.FindAsync(id);
            productDetails.IsDeleted = true;
            productDetails.DeletedBy = _currentUser.UserId;
            productDetails.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
