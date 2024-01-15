using Core.Entities;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	
	public class Image : EntityBase, IEntityBase
	{
		public Image() { }
		public Image(string fileName, string fileType, string createdBy) 
		{
			FileName = fileName;
			FileType = fileType;
			CreatedBy = createdBy;
		 
		}
		public string FileName { get; set; }	
		public string FileType { get; set; }
		public ICollection<Article> Articles { get; set; }
		public ICollection<AppUser> AppUsers { get; set; }

	}
}
