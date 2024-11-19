using Application.Dtos.Posts;
using Application.Services.Posts;
using Application.Settings;
using Application.Validators.Posts;
using AutoMapper;
using Domian.Entities;
using HaveBreakTest.Api.Extensions;
using HaveBreakTest.Api.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HaveBreakTest.Api.Controllers
{
	
	public class PostsController : HaveBreakController
	{
		private readonly IMapper _mapper;
		private readonly IPostService _postService;
		private readonly string _imagesPath;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMemoryCache _cache;
		private readonly string cacheKey = "feeds";
		public PostsController(IMapper mapper, IPostService postService, IWebHostEnvironment webHostEnvironment,IMemoryCache cache )
		{
			_mapper = mapper;
			_postService = postService;
			_webHostEnvironment = webHostEnvironment;
			_imagesPath = $"{_webHostEnvironment.WebRootPath}/{FileSettings.ImagesPath}";
			_cache = cache;
		}

		[HttpGet]
		[Route("feeds")]
		public async Task<IActionResult> GetAllFeedsAsync()
		{
			if(_cache.TryGetValue(cacheKey,out List<Post>? entities)){

			}
			else
			{
			 entities = (await _postService.GetAllFeedAsync());

				var cacheOptions = new MemoryCacheEntryOptions()
								   .SetSlidingExpiration(TimeSpan.FromSeconds(45))
								   .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
								   .SetPriority(CacheItemPriority.Normal);
				_cache.Set(cacheKey, entities, cacheOptions);
			}
						   

			var dtos = _mapper.Map<List<FeedDto>>(entities);

			return Ok(new ListDataResponse<FeedDto>(entities!.Count, dtos));
		}
		[HttpGet]
		[Route("{postId}")]
		public async Task<IActionResult> GetPostByIdAsync(int postId)
		{
			var entity = await _postService.GetByIdAsync(postId);
			if (entity == null)
				return NotFound();

			var dto = _mapper.Map<PostDto>(entity);

			return Ok(new DataResponse<PostDto>(true,dto));
		}
		[HttpPost]
		[Route("")]
		public async Task<IActionResult> CreatePostAsync([FromForm] CreateOrUpdatePostDto dto)
		{
			try
			{

			var validator = new CreateOrUpdatePostDtoValidator(_postService);
			var result = await validator.ValidateAsync(dto);
			if (!result.IsValid)
				return InvalidFormData(new DataResponse(false, GetErrorMessages(result)));
			dto.ImageName= await SaveImage(dto.Image);
			var post =await _postService.AddAsync(dto, User.GetUserId());
			

			return ResourceCreated(new DataResponse<PostDto>(true, _mapper.Map<PostDto>(post),"created"));
			}
			catch (Exception)
			{

				return InvalidDataResult(new DataResponse(false, "Internal Server Error"));
			}
		}
		[HttpPost("Like")]
		public async Task<ActionResult> AddLikeToPos(int postId)
		{
			var validator = new AddLikeToPostValidator(_postService);
			var result = await validator.ValidateAsync(postId);
			if (!result.IsValid)
				return InvalidFormData(new DataResponse(false, GetErrorMessages(result)));
			await _postService.AddLike(postId, User.GetUserId());


			return ResourceCreated(new DataResponse(true));
		}

		#region Helpers
		private async Task<string> SaveImage(IFormFile? cover)
		{
			if (cover != null)
			{
				var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

				var path = Path.Combine(_imagesPath, coverName);

				using var stream = System.IO.File.Create(path);
				await cover.CopyToAsync(stream);

				return coverName;

			}
			return string.Empty;
		}
		#endregion
	}
}
