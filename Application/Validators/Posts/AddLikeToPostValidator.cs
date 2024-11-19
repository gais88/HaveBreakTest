using Application.Services.Posts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Posts
{
	public class AddLikeToPostValidator:AbstractValidator<int>
	{
        private readonly IPostService _postService;
        public AddLikeToPostValidator(IPostService postService)
        {
            _postService = postService;

			RuleFor(id => id)
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
