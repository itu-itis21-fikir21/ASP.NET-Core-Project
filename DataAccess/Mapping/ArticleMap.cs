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
	public class ArticleMap : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.HasData(new Article
			{
				Id = Guid.NewGuid(),
				Title = "Asp.Net Core Sample Article 1",
				Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.\r\n\r\n",
				ViewCount = 20,
				CategoryId = Guid.Parse("43B95956-A86A-4D64-A8C5-F8C67C7100E1"),

				ImageId = Guid.Parse("F144F00B-5A1E-4624-A3CC-DD26B2E0CEE9"),
				
				CreatedBy =	"admin",
				CreatedDate = DateTime.Now,
				IsDeleted = false,

                UserId = Guid.Parse("3C59641E-B5E6-40CA-98F4-D7E489EB2588"),

            },
			new Article
			{
				Id = Guid.NewGuid(),
				Title = "Asp.Net Core Sample Article 2",
                Content = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.\r\n\r\n",
				ViewCount = 20,
				CategoryId = Guid.Parse("40CD793E-8D26-4CC5-8E56-8AB18093DE1C"),
				
				ImageId = Guid.Parse("3BD57681-D153-4934-83F7-3458A9716648"),
				
				CreatedBy = "admin",
				CreatedDate = DateTime.Now,
				IsDeleted = false,
                UserId = Guid.Parse("C7DE37C5-4666-4B76-8933-A4871C8A114B"),

            }



			); 

		}
	}
}
