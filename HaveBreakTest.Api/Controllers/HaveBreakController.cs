using FluentValidation.Results;
using HaveBreakTest.Api.Common.CustomHttpResults;
using HaveBreakTest.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaveBreakTest.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class HaveBreakController : ControllerBase
	{

		public HaveBreakController()
		{ }
            
        protected internal virtual ResourceCreatedResult<T> ResourceCreated<T>(T content)
		{
			return new ResourceCreatedResult<T>(content);
		}
		protected internal virtual ResourceUpdatedResult<T> ResourceUpdated<T>(T content)
		{
			return new ResourceUpdatedResult<T>(content);
		}
		
		protected internal virtual InvalidFormDataResult<T> InvalidFormData<T>(T content)
		{
			return new InvalidFormDataResult<T>(content);
		}
		protected internal virtual InvalidDataResult<T> InvalidDataResult<T>(T content)
		{
			return new InvalidDataResult<T>(content);
		}
		protected internal virtual NotAuthorizedResult<T> NotAuthorized<T>(T content)
		{
			return new NotAuthorizedResult<T>(content);
		}
		protected internal virtual BadRequestResult<T> BadRequest<T>(T content)
		{
			return new BadRequestResult<T>(content);
		}
		protected internal static string GetErrorMessages(ValidationResult result)
		{
			return string.Join("\n", result.Errors.Select(x => x.ErrorMessage)) + "\n";
		}
	}
}
