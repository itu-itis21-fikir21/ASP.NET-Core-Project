using DataAccess.Repositories;
using DataAccess.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
	public static class DataLayerExtensions
	{
		public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config) 
		{
			services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
			services.AddDbContext<RepositoryContext>(x=>x.UseSqlServer(config.GetConnectionString("sqlConnection")));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}
