using Application.Services.Comments;
using Application.Services.Posts;
using Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IPostService, PostService>();
			services.AddTransient<ICommentService, CommentService>();
			return services;


		}
	}
}
