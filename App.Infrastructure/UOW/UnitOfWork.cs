using App.Application.Interfaces;
using App.Application.Interfaces.IRepositories;
using App.Application.Services;
using App.Domain.Entities;
using App.Infrastructure.Persistence;
using App.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public UnitOfWork(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public IUserRepository _users;
        public IProductRepository _products;
        public IOrderRepository _orders;
        public ICategoryRepository _categories;
        public IOrderDetailRepository _orderDetails;
        public IProductCategoryRepository _productCategories;

        public IUserRepository Customers
        {
            get
            {
                if (_users is null)
                   return new UserRepository(_context);
                return _users;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if( _products is null)
                    return new ProductRepository(_context, _currentUserService);
                return _products;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orders is null)
                    return new OrderRepository(_context);
                return _orders;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if(_categories is null)
                    return new CategoryRepository(_context);
                return _categories;
            }
        }

        public IOrderDetailRepository OrderDetails
        {
            get
            {
                if(_orderDetails is null)
                    return new OrderDetailRepository(_context);
                return _orderDetails;
            }
        }
        public IProductCategoryRepository ProductCategories 
        {
            get
            {
                if(_productCategories is null)
                    return new ProductCategoryRepository(_context);
                return _productCategories;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DisposableAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
