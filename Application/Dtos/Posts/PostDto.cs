using Application.Dtos.Comments;
using Domian.Entities;
using Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Posts
{
	public class PostDto
	{
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string? Image { get; set; } = string.Empty;
		public DateTime PostDate { get; set; }
		public int AppUserId { get; set; }
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public int Like { get; set; }
		public PostType PostType { get; set; }
		public virtual ICollection<CommentDto>? Comments { get; set; }
	}
}
