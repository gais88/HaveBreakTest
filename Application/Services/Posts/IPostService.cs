using Application.Dtos.Posts;
using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Posts
{
	public interface IPostService : ICrudService<Post, int, CreateOrUpdatePostDto>
	{
		Task<List<Post>> GetAllFeedAsync();
		Task<bool> AddLike(int postId,int userId);
		Task<bool> IsPostExistenceAsync(int PostId);
	}
}
