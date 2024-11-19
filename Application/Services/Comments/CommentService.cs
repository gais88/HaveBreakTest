using Application.Dtos.Comments;
using Domian.Entities;
using Infrastructure.DbContexts;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Comments
{
	public class CommentService : CrudService<Comment, int, CreateOrUpdateCommentDto>, ICommentService
	{
        public CommentService(AppDbContext context, IMasterUnitOfWork UnitOfWork) : base(context, UnitOfWork)
		{

		}
		public async override Task<Comment> AddAsync(CreateOrUpdateCommentDto dto, int userId)
		{
			
			await _unitOfWork.BeginTransactionAsync();

			var entity = new Comment
			{
				Content = dto.Content,
				PostId = dto.PostId,
				AppUserId = userId,
				CreatedOn = DateTime.UtcNow,
				CreatedBy = userId,

			};
			await _repository.AddAsync(entity);
			await _unitOfWork.CommitTransactionSaveChangesAsync();

			return entity;

			
			
		}

		public async override Task<Comment> GetByIdAsync(int ID)
		{
			return await _repository.GetFirstAsync(x => x.Id == ID);
		}

		public override Task<Comment> UpdateAsync(int ID, CreateOrUpdateCommentDto dto, int userId)
		{
			throw new NotImplementedException();
		}
	}
}
