using Application.Dtos.Posts;
using Application.Services.Posts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Posts
{
	public class CreateOrUpdatePostDtoValidator: AbstractValidator<CreateOrUpdatePostDto>
	{
        public CreateOrUpdatePostDtoValidator(IPostService postService)
        {
			RuleFor(dto => dto.Title).NotEmpty().WithMessage("Title is Required ")
									.MinimumLength(4).WithMessage("Title  min lenth 4 ")
									.MaximumLength(50).WithMessage("Title   max 50");
			RuleFor(dto => dto.PostType).NotNull()
				                       .WithMessage("PostType is Required");
		}
    }
}
