using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
	public interface IRepository<T, TDbContext>
		where T : class
		where TDbContext : DbContext
	{
		Task<T> AddAsync(T entity);
		Task AddRangeAsync(IEnumerable<T> entities);
		Task<bool> AnyAsync(Expression<Func<T, bool>> func);
		List<T> GetAll();
		Task<List<T>> GetAllAsync();
		T GetFirst(Expression<Func<T, bool>> func);
		Task<T> GetFirstAsync(Expression<Func<T, bool>> func);
		Task<TResult?> GetFirstAsync<TResult>(Expression<Func<T, TResult>> selector);
		Task<TResult?> GetFirstAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector);
		Task<T?> GetFirstOrDefaultAsync();
		Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> func);
		Task<T?> GetLastAsync();
		Task<TResult?> GetLastAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector);
		Task<TResult?> GetLastAsync<TResult>(Expression<Func<T, TResult>> selector);
		Task<List<T>> GetListAsync(Expression<Func<T, bool>> func);
		Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector);
		Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector);
		Task<List<T>> GetListPaginatedAsync(Expression<Func<T, bool>> func);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
		Task RemoveWhereAsync(Expression<Func<T, bool>> func);
	}

}
