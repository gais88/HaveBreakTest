using Infrastructure.DataAccess;
using Infrastructure.DbContexts;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public abstract class CrudService<T, TKey, TCreateOrUpdateDto>
	: CrudService<T, AppDbContext>
	, ICrudService<T, TKey, TCreateOrUpdateDto> where T : class
	{
		public CrudService(AppDbContext context, IMasterUnitOfWork UnitOfWork)
			: base(context, UnitOfWork)
		{
		}

		public abstract Task<T> GetByIdAsync(TKey ID);
		public abstract Task<T> AddAsync(TCreateOrUpdateDto dto, int userId);
		public abstract Task<T> UpdateAsync(TKey ID, TCreateOrUpdateDto dto, int userId);

		
		
		
		
	}

	public abstract class CrudService<T, TDbContext> : ICrudService<T, TDbContext>
		where T : class
		where TDbContext : DbContext
	{
		protected readonly TDbContext _dbContext;
		protected readonly IUnitOfWork<TDbContext> _unitOfWork;
		protected readonly IRepository<T, TDbContext> _repository;

		public CrudService(TDbContext dbContext, IUnitOfWork<TDbContext> unitOfWork)
		{
			_dbContext = dbContext;
			_unitOfWork = unitOfWork;
			_repository = new Repository<T, TDbContext>(dbContext);
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<T> GetFirstAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.GetFirstAsync(func);
		}

		public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.GetFirstOrDefaultAsync(func);
		}

		public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> func)
		{
			return await _repository.GetListAsync(func);
		}

		public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> func, Expression<Func<T, TResult>> selector)
		{
			return await _repository.GetListAsync(func, selector);
		}

		public async Task SaveChangesAsync()
		{
			await _unitOfWork.CommitTransactionSaveChangesAsync();
		}
	}

}
