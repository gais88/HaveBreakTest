using Domian.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Comments
{
	public class CommentDto
	{
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
		public int PostId { get; set; }
		
		public int AppUserId { get; set; }
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
	}
}
