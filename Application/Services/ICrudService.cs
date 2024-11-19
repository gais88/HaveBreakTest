using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public interface ICrudService<T, TKey, TCreateOrUpdateDto> : ICrudService<T, AppDbContext>
	where T : class
	{
		Task<T> GetByIdAsync(TKey ID);
		Task<T> AddAsync(TCreateOrUpdateDto dto, int userId);
		Task<T> UpdateAsync(TKey ID, TCreateOrUpdateDto dto, int userId);
		
	}

	public interface ICrudService<T, TDbContext>
		where T : class
		where TDbContext : DbContext
	{
		Task<List<T>> GetAllAsync();
		Task<T> GetFirstAsync(Expression<Func<T, bool>> func);
		Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> func);
		Task<List<T>> GetListAsync(Expression<Func<T, bool>> func);
		Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector);

	}

}
