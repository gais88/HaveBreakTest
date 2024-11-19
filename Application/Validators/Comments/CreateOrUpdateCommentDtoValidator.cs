using Application.Dtos.Comments;
using Application.Dtos.Posts;
using Application.Services.Comments;
using Application.Services.Posts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Comments
{
	public class CreateOrUpdateCommentDtoValidator : AbstractValidator<CreateOrUpdateCommentDto>
	{
		private readonly IPostService _postService;
       
        public CreateOrUpdateCommentDtoValidator(IPostService postServic)

		{
			_postService = postServic;


			RuleFor(dto => dto.Content).NotEmpty().WithMessage("Content is required")
									   .MinimumLength(2).WithMessage("Contetn Minumum 1 charcter")
				                       .MaximumLength(50).WithMessage("Contetnt  max 50");

			RuleFor(dto => dto.PostId)
				                    .NotNull().WithMessage("post Id  is Required")
									.MustAsync((postId, ct) => ValidatePostExistenceAsync(postId, ct))
			                        .WithMessage("Post Id is Not Exist"); 
		}

		private async Task<bool> ValidatePostExistenceAsync(int postId, CancellationToken ct)
		{
			return await _postService.IsPostExistenceAsync(postId);
		}
	}
}
