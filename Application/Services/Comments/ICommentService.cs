using Application.Dtos.Comments;
using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Comments
{
	public interface ICommentService : ICrudService<Comment,int, CreateOrUpdateCommentDto>
	{
		
	}
}
