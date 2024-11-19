﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace HaveBreakTest.Api.Common.CustomHttpResults
{
	public class NotAuthorizedResult<T> : ActionResult
	{
		private readonly T _content;
		private readonly HttpStatusCode _statusCode;

		public NotAuthorizedResult(T content, HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
		{
			_content = content;
			_statusCode = statusCode;
		}

		public override async Task ExecuteResultAsync(ActionContext context)
		{
			var response = context.HttpContext.Response;
			response.StatusCode = (int)_statusCode;
			response.ContentType = "application/json";
			var json = JsonSerializer.Serialize(_content);
			await response.WriteAsync(json);
		}
	}
}
