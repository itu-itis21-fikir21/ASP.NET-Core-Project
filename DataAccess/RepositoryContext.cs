using Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DataAccess
{
	public class RepositoryContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
	{
		public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
		{
		}
		public DbSet<Article> Articles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Visitor> Visitors { get; set; }
		public DbSet<ArticleVisitor> ArticleVisitors { get; set; }	


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);	
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	    }
	}

}
