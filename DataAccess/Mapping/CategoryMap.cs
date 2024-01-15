using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
	public class CategoryMap : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasData(new Category
			{
				Id = Guid.Parse("43B95956-A86A-4D64-A8C5-F8C67C7100E1"),
				Name = "Sample Category 1",
				CreatedBy = "admin",
				CreatedDate = DateTime.Now,
				IsDeleted = false,
			},
			new Category
			{
				Id = Guid.Parse("40CD793E-8D26-4CC5-8E56-8AB18093DE1C"),
				Name = "Sample Category 2",
				CreatedBy = "admin",
				CreatedDate = DateTime.Now,
				IsDeleted = false,
			}
			);
		}
	}
}
