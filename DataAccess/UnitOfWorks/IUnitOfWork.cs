using Core.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWorks
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IRepositoryBase<T> GetRepository<T>() where T : class, IEntityBase, new();
		Task<int> SaveAsync();
		int Save();
	}
}
