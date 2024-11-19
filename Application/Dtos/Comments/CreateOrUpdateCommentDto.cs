using Domian.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Comments
{
	public class CreateOrUpdateCommentDto
	{

		public string Content { get; set; } = string.Empty;
		
		public int PostId { get; set; }
		
		
		
	}
}
