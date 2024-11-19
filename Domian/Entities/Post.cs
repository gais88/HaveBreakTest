using Domian.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities
{
	public class Post:AuditEntity
	{
		[Required, MaxLength(50)]
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string? Image { get; set; } = string.Empty;
        public DateTime PostDate { get; set; }
        public int AppUserId { get; set; } 
		public virtual AppUser AppUser { get; set; } = default!;
		public int Like { get; set; }
        public PostType	PostType { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
	}
}
