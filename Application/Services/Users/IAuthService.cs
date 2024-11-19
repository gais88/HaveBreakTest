using Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users
{
	public interface IAuthService
	{
		Task<UserDto> RegisterAsync(RegisterDto model);
		Task<UserDto> LoginAsync(LoginDto model);
	}
}
