using Application.Dtos.Posts;
using Domian.Entities;
using Infrastructure.DbContexts;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Posts
{
	public class PostService : CrudService<Post, int, CreateOrUpdatePostDto>, IPostService
	{
		
		public PostService(AppDbContext context, IMasterUnitOfWork UnitOfWork) : base(context, UnitOfWork)
		{
			
		}

		public async override Task<Post> AddAsync(CreateOrUpdatePostDto dto, int userId)
		{
			
			await _unitOfWork.BeginTransactionAsync();

			var entity = new Post
			{
				Title = dto.Title,
				Content = dto.Content,
				PostDate = DateTime.UtcNow,
				Image = dto.ImageName,
				AppUserId = userId,
				CreatedOn = DateTime.UtcNow,
				CreatedBy = userId,
				
			};
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitTransactionSaveChangesAsync();

			return entity;
			

		}

		public async Task<bool> AddLike(int postId,int userId)
		{
			

			await _unitOfWork.BeginTransactionAsync();

			var entity = await GetByIdAsync(postId);
			entity.Like += 1;
			entity.ModifiedOn = DateTime.UtcNow;
			entity.ModifiedBy = userId;
			await _unitOfWork.CommitTransactionSaveChangesAsync();

			return true;
			
			
		}

		public async Task<List<Post>> GetAllFeedAsync()
		{
			var feeds = await _repository.GetAllAsync();
			return feeds.OrderByDescending(x => x.PostDate).ToList();
		}

		public async override Task<Post> GetByIdAsync(int ID)
		{
			return await _repository.GetFirstAsync(x => x.Id == ID);
		}

		public async Task<bool> IsPostExistenceAsync(int PostId)
		{
			return await _repository.AnyAsync(x => x.Id == PostId);
		}

		public override Task<Post> UpdateAsync(int ID, CreateOrUpdatePostDto dto, int userId)
		{
			throw new NotImplementedException();
		}
	}
}
