using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RepositoryBase<T> : IRepositoryBase<T>
		where T : class, IEntityBase, new()
	{
		private readonly RepositoryContext _context;
		public RepositoryBase(RepositoryContext context)
		{
			_context = context;
		}

		private DbSet<T> Table { get => _context.Set<T>(); }


		public async Task Add(T entity)
		{
			await Table.AddAsync(entity);
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await Table.AnyAsync(predicate);	
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
		{
            if (predicate is not null)
                return await Table.CountAsync(predicate);
            return await Table.CountAsync();
        }

		public async Task Delete(T entity)
		{
			await Task.Run(() => Table.Remove(entity));
		}

		public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> query = Table;
			if(predicate != null)
				query = query.Where(predicate);
			if (includeProperties.Any())
				foreach (var item in includeProperties)
					query = query.Include(item);
			return await query.ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> query = Table;
			query = query.Where(predicate);
			if (includeProperties.Any())
				foreach (var item in includeProperties)
					query = query.Include(item);
			return await query.SingleAsync();
		}

		public async Task<T> GetById(Guid id)
		{
			return await Table.FindAsync(id);
		}

		public async Task<T> Update(T entity)
		{
			await Task.Run(() => Table.Update(entity));
			return entity;
		}
	}
}
