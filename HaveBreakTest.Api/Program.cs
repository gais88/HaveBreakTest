
using Infrastructure;
using Application;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using HaveBreakTest.Api.Extensions;
using System.Text.Json.Serialization;
using HaveBreakTest.Api.Middelwares;

namespace HaveBreakTest.Api
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddSwaggerGen();
			builder.Services.AddIdentityServices(builder.Configuration);
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddApplicationServices();
			builder.Services.AddSwaggerDocumentation();
			//builder.Services.AddResponseCaching();
			 builder.Services.AddMemoryCache();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware< InitBaseUrlMiddleware>();

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();
			//app.UseResponseCaching();

			app.MapControllers();
			
			// Data Seed
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<AppDbContext>();
			try
			{
				if (context != null)
				{
					await context.Database.MigrateAsync();
				}

			}
			catch (Exception ex)
			{
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "Error Occured During Migration");
			}

			app.Run();
		}
	}
}
