using App.Application.Interfaces.IRepositories;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
             _entities.Add(entity);
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return  _entities.AsQueryable();
        }

        public TEntity GetById(Guid id)
        {
            return _entities.Find(id);
        }

        public TEntity Update(TEntity entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return null;
        }
    }
}
