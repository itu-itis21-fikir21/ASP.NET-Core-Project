using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public abstract class EntityBase : IEntityBase
	{
		public virtual Guid Id { get; set; }= Guid.NewGuid();
		public virtual string CreatedBy { get; set; } = "undefined";
		public virtual string? DeletedBy { get; set; }
		public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
		public virtual DateTime? DeletedDate { get; set; }
		public virtual bool IsDeleted { get; set; }	= false;
	}

}
