using Domian.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Posts
{
	public class CreateOrUpdatePostDto
	{
		
		public string Title { get; set; } = string.Empty;
		
		public string Content { get; set; } = string.Empty;
		public IFormFile? Image { get; set; }
		public PostType PostType { get; set; }

		[JsonIgnore]
		public  string? ImageName;
	}
}
