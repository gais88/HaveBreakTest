
using Application.Dtos.Comments;
using AutoMapper;
using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
	public class CommentProfile : Profile
	{
        public CommentProfile()
        {
			CreateMap<Comment, CommentDto>()
			   .ForMember(viewModel => viewModel.FirstName, model => model.MapFrom(model => model.AppUser.FirstName))
			   .ForMember(viewModel => viewModel.LastName, model => model.MapFrom(model => model.AppUser.LastName));

		}
    }
}
