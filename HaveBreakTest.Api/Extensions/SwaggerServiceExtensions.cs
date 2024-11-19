using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace HaveBreakTest.Api.Extensions
{
	public static class SwaggerServiceExtensions
	{
		public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{

				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "HaveBreak Test  App",
					Version = "v1",
					Description = "API for Testing Purpose Only.",
					Contact = new OpenApiContact()
					{
						Name = "HaveBreak",
						Email = "HaveBreak@localhost.net",
					},

				});

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Jwt Auth Header",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference()
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});

				c.TagActionsBy(api =>
				{
					if (api.GroupName != null)
					{
						return new[] { api.GroupName };
					}

					if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
					{
						return new[] { controllerActionDescriptor.ControllerName };
					}

					throw new InvalidOperationException("Unable to determine tag for endpoint.");
				});

				c.DocInclusionPredicate((name, api) => true);

				// c.UseDateOnlyTimeOnlyStringConverters();
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HaveBreak API v1"));

			return app;
		}
	}
}

