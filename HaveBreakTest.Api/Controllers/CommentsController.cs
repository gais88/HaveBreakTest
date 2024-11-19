using Application.Dtos.Comments;
using Application.Dtos.Posts;
using Application.Services.Comments;
using Application.Services.Posts;
using Application.Validators.Comments;
using Application.Validators.Posts;
using AutoMapper;
using Domian.Entities;
using HaveBreakTest.Api.Extensions;
using HaveBreakTest.Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace HaveBreakTest.Api.Controllers
{
	
	public class CommentsController : HaveBreakController
	{
		
		private readonly ICommentService _commentService;
		private readonly IPostService _postService;
		private readonly IMapper _mapper;

		public CommentsController(ICommentService commentService, IPostService postService, IMapper mapper)
		{

			_commentService = commentService;
			_postService = postService;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> CreateCommentAsync([FromBody] CreateOrUpdateCommentDto dto)
		{
			try
			{

			var validator = new CreateOrUpdateCommentDtoValidator(_postService);
			var result = await validator.ValidateAsync(dto);
			if (!result.IsValid)
				return InvalidFormData(new DataResponse(false, GetErrorMessages(result)));
			
			var comment = await _commentService.AddAsync(dto, User.GetUserId());


			return ResourceCreated(new DataResponse<CommentDto>(true, _mapper.Map<CommentDto>(comment),"created"));
			}
			catch (Exception)
			{

				return InvalidDataResult(new DataResponse(false, "Internal Server Error"));
			}
		}

	}
}
