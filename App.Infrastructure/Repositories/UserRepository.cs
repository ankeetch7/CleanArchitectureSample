using App.Application.Interfaces.IRepositories;
using App.Application.Services;
using App.Domain.Entities;
using App.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<Customer>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    
    }
}
