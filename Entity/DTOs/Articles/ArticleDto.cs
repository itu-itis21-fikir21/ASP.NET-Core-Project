

using Core.Entities;
using Entity.DTOs.Categories;
using Entity.Entities;
using System.Globalization;

namespace Entity.DTOs.Articles
{
	public class ArticleDto : IEntityBase
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public CategoryDto Category { get; set; }
		public Image Image { get; set; }
		public AppUser User { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }
    }
}
