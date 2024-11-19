using Application.Settings;
using Azure.Core;

namespace HaveBreakTest.Api.Middelwares
{
	public class InitBaseUrlMiddleware
	{
		private readonly RequestDelegate _next;
        public InitBaseUrlMiddleware(RequestDelegate next)
        {
			_next = next;

		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				
				GeneralSetting.BaseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
				await _next(context);
			}
			catch (Exception)
			{
			}
		}
	}
}
