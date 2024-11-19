using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities
{
	public class AppUser: IdentityUser<int>
	{
		[RegularExpression(@"^[a-zA-Z]*$",
		ErrorMessage = "First Name must contain only letters")]
		[Display(Name = "First Name")]

		[Length(3, 25)]
		public string FirstName { get; set; } = string.Empty;
		[RegularExpression(@"^[a-zA-Z]*$",
		ErrorMessage = "Last Name must contain only letters")]
		[Display(Name = "Last Name")]

		[Length(3, 25)]
		public string LastName { get; set; } = string.Empty;
		public virtual ICollection<Post>? Posts { get; set; }
	}
}
