

using Infrastructure.DbContexts;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IMasterUnitOfWork, MasterUnitOfWork>();

			
			//services.AddDbContext<AppDbContext>((sp, options) =>
			//{
			//	;
			//	options
			//	.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"))
			//	.UseLazyLoadingProxies()
			//	.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
			//	.EnableSensitiveDataLogging()
			//	;
			//});

			// comment this section 
			services.AddDbContext<AppDbContext>((sp, options) =>
			{
				
				options
				.UseSqlServer(configuration.GetConnectionString("MasterConnection"), opt => opt.UseCompatibilityLevel(110))
				.UseLazyLoadingProxies()
				.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
				.EnableSensitiveDataLogging()
				;
			});

			

			return services;
		}
	}

}
