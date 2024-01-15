using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepositoryBase<T> where T : class, IEntityBase, new()
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

		Task<T> GetById(Guid id);

		Task Add(T entity);

        Task Delete(T entity);
        
        Task<T> Update(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate=null);
    }
}
