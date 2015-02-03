using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Capstone.Core
{
    public class Repository : IRepository
    {

        private readonly DbContext _dbContext;

        public DbContext Context { get; private set; }
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            Context = _dbContext;
        }

        public Repository()
        {
        }

        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(_dbContext.Set<TEntity>().Find(predicate));
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(_dbContext.Set<TEntity>().Find(predicate));
            await _dbContext.SaveChangesAsync();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }

}