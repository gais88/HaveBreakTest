using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
	public class Repository<T, TDbContext> : IRepository<T, TDbContext>
	where T : class
	where TDbContext : DbContext
	{
		private readonly TDbContext _dbContext;
		private readonly DbSet<T> _repository;

		public Repository(TDbContext dbContext)
		{
			_dbContext = dbContext;
			_repository = _dbContext.Set<T>();

		}
		public async Task<T> AddAsync(T entity)
		{
			await _repository.AddAsync(entity);

			

			return entity;
		}
		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			if (entities.Any() == false)
				return;

			await _repository.AddRangeAsync(entities);

			
		}
		public async Task<bool> AnyAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.AnyAsync(func);
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _repository.AsNoTracking().ToListAsync();
		}
		public List<T> GetAll()
		{
			return _repository.ToList();
		}
		public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.AsNoTracking().Where(func).ToListAsync();
		}
		public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector)
		{
			return await _repository.AsNoTracking().Select(selector).ToListAsync();
		}
		public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector)
		{
			return await _repository.AsNoTracking().Where(func).Select(selector).ToListAsync();
		}
		public async Task<List<T>> GetListPaginatedAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.AsNoTracking().Where(func).ToListAsync();
		}
		public async Task<T?> GetFirstOrDefaultAsync()
		{
			return await _repository.FirstOrDefaultAsync();
		}
		public async Task<T> GetFirstAsync(Expression<Func<T, bool>> func)
		{
			var entity = await _repository.Where(func).FirstOrDefaultAsync();
			return entity ?? throw new NullReferenceException();
		}
		public T GetFirst(Expression<Func<T, bool>> func)
		{
			var entity = _repository.Where(func).FirstOrDefault();
			return entity ?? throw new NullReferenceException();
		}
		public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.Where(func).FirstOrDefaultAsync();
		}
		public async Task<TResult?> GetFirstAsync<TResult>(Expression<Func<T, TResult>> selector)
		{
			return await _repository.Select(selector).FirstOrDefaultAsync();
		}
		public async Task<TResult?> GetFirstAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector)
		{
			return await _repository.Where(func).Select(selector).FirstOrDefaultAsync();
		}
		public async Task<T?> GetLastAsync()
		{
			return (await GetAllAsync()).LastOrDefault();
		}
		public async Task<TResult?> GetLastAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector)
		{
			return (await GetListAsync(func, selector)).LastOrDefault();
		}
		public async Task<TResult?> GetLastAsync<TResult>(Expression<Func<T, TResult>> selector)
		{
			return (await GetListAsync(selector)).LastOrDefault();
		}
		public void Remove(T entity)
		{
			_repository.Remove(entity);
		}
		public void RemoveRange(IEnumerable<T> entities)
		{
			if (entities.Any() == false)
				return;
			_repository.RemoveRange(entities);
		}
		public async Task RemoveWhereAsync(Expression<Func<T, bool>> func)
		{
			var entities = await _repository.Where(func).ToListAsync();
			if (entities.Count == 0)
				return;

			_repository.RemoveRange(entities);
		}
	}

}
