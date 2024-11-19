using Application.Dtos.Users;
using Application.Services.Users;
using HaveBreakTest.Api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaveBreakTest.Api.Controllers
{
	[AllowAnonymous]
	public class AccountController : HaveBreakController
	{


		private readonly IAuthService _authService;
		public AccountController(IAuthService authService)
		{

			_authService = authService;
		}
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			try
			{

			if (!ModelState.IsValid)
				return InvalidDataResult(ModelState);

			var result = await _authService.RegisterAsync(model);

			if (!result.IsAuthenticated)
				return NotAuthorized(new DataResponse(false, result.Message));

			return ResourceCreated(new DataResponse<UserDto>(true, result, "User Was Created Succefully"));
			}
			catch (Exception)
			{

				return InvalidDataResult(new DataResponse(false, "Internal Server Error"));
			}
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			try
			{
			if (!ModelState.IsValid)
				return InvalidDataResult (ModelState);

			var result = await _authService.LoginAsync(model);

			if (!result.IsAuthenticated)
				return NotAuthorized(new DataResponse(false, result.Message));

			return ResourceCreated(new DataResponse<UserDto>(true, result, "Login Success"));

			}
			catch (Exception)
			{

				return InvalidDataResult(new DataResponse(false, "Internal Server Error"));
			}

		}


	}
}