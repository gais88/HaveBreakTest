using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities
{
	public class Comment :AuditEntity
	{
		[Required, MaxLength(250)]
		public string Content { get; set; } = string.Empty;
		[Required]
		public int PostId { get; set; }
		public virtual Post Post { get; set; } = default!;
		[Required]
		public int AppUserId { get; set; } 
		public virtual AppUser AppUser { get; set; } = default!;
	}
}
