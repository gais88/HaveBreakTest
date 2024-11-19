using Application.Dtos.Posts;
using Application.Settings;
using AutoMapper;
using Domian.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
	public class PostProfile : Profile
	{
        public PostProfile()
        {

			CreateMap<Post, FeedDto>()
			   .ForMember(viewModel => viewModel.ShortContent, model => model.MapFrom(model => model.Content.Length >GeneralSetting.FeedLength ? $"{model.Content.Substring(0, GeneralSetting.FeedLength)} ...." : model.Content))
			   .ForMember(viewModel => viewModel.ImageUrl, model => model.MapFrom(model => !string.IsNullOrEmpty(model.Image)?($"{GeneralSetting.BaseUrl}/{FileSettings.ImagesPath}/{model.Image}") :""))
			   .ForMember(viewModel => viewModel.FirstName, model => model.MapFrom(model => model.AppUser.FirstName))
			   .ForMember(viewModel => viewModel.LastName, model => model.MapFrom(model => model.AppUser.LastName))
			   
			   .ReverseMap();

			CreateMap<Post, PostDto>()
			   .ForMember(viewModel => viewModel.Image, model => model.MapFrom(model => !string.IsNullOrEmpty(model.Image) ? ($"{GeneralSetting.BaseUrl}/{FileSettings.ImagesPath}/{model.Image}") : ""))
			   .ForMember(viewModel => viewModel.FirstName, model => model.MapFrom(model => model.AppUser.FirstName))
			   .ForMember(viewModel => viewModel.LastName, model => model.MapFrom(model => model.AppUser.LastName))

			   .ReverseMap();

			CreateMap<Post, CreateOrUpdatePostDto>()
				
				.ReverseMap();
		}
    }
}
