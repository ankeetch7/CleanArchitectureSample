using App.Application.Command.Product.CreateProduct;
using App.Application.Command.Product.UpdateProduct;
using App.Application.Error;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task CreateProduct(CreateProductCommand command);
        Task UpdateProduct(UpdateProductCommand command);
        Task DeleteProduct(Guid id);
        Task<IEnumerable<ProductVm>> GetAllProducts(); 
    }
}
