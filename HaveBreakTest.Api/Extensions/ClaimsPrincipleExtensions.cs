﻿using System.Security.Claims;

namespace HaveBreakTest.Api.Extensions
{
	public static class ClaimsPrincipleExtensions
	{
		

		public static  int GetUserId(this ClaimsPrincipal user)
		{
			
			return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		}
	}
}
