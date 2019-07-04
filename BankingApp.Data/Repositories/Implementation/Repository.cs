using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BankingApp.Data.Repositories.Interfaces;

namespace BankingApp.Data.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> Entity;

        public Repository(DbContext context)
        {
            _context = context;
            Entity = context.Set<TEntity>();
        }

        public TEntity GetById(Guid id)
        {
            return Entity.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entity.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entity.Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            Entity.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entity.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
        }
    }
}