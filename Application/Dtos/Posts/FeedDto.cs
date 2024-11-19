using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Posts
{
	public class FeedDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;
		public int Like { get; set; }
		public string ShortContent { get; set; } = string.Empty;
		public DateTime PostDate { get; set; }
		public string? ImageUrl { get; set; } = string.Empty;
		public string AppUserId { get; set; } = string.Empty;
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
	}
}
