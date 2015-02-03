using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Core
{
    public interface IRepository
    {
        DbContext Context { get; }
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        TEntity Add<TEntity>(TEntity source) where TEntity : class;
        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void SaveChanges();
    }
}