using App.Application.Interfaces.IRepositories;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Customers { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICategoryRepository Categories { get; }
        IOrderDetailRepository OrderDetails { get; }
        IProductCategoryRepository ProductCategories { get; }
        Task SaveChangesAsync();
        Task DisposableAsync();
    }
}
