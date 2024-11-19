using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Users
{
	public class UserDto
	{
		public int Id { get; set; } 
		public string Email { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
        public DateTime TokenExpireDate { get; set; }
        public bool IsAuthenticated { get; set; }
        
    }
}
