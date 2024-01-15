using Core.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWorks
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly RepositoryContext _context;
        public UnitOfWork(RepositoryContext context)
        {
            _context = context;
        }
        public ValueTask DisposeAsync()
		{
			return _context.DisposeAsync();
		}

		IRepositoryBase<T> IUnitOfWork.GetRepository<T>()
		{
			return new RepositoryBase<T>(_context);
		}

		public int Save()
		{
			return _context.SaveChanges(); 
		}

		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
