using Domian.Entities;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HaveBreakTest.Api.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
			// CORS Configurations.
			services.AddCors(option =>
			{
				option.AddPolicy("CorsePolicy", policy =>
				{
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowAnyOrigin();
				});
			});

			// Add Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
					.AddCookie()
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["ApiKey"]!)),
							ValidateIssuer = false,
							ValidateAudience = false
						};
					});

			// Add Identity
			services.AddIdentityCore<AppUser>(option =>
			{
				option.Password.RequireNonAlphanumeric = false;
				option.Password.RequireDigit = true;
				option.Password.RequireUppercase = false;
			})
			.AddRoles<IdentityRole<int>>()
			.AddRoleManager<RoleManager<IdentityRole<int>>>()
			.AddSignInManager<SignInManager<AppUser>>()
			.AddRoleValidator<RoleValidator<IdentityRole<int>>>()
			.AddEntityFrameworkStores<AppDbContext>();

			return services;
		}
	}
}
