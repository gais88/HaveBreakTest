using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace Infrastructure.UnitOfWorks
{
	public  class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
	where TDbContext : DbContext
	{
		protected TDbContext _dbContext;
		private IDbContextTransaction? _transaction;

		public UnitOfWork(TDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task BeginTransactionAsync()
		{
			_transaction ??= await _dbContext.Database.BeginTransactionAsync();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				if (_transaction != null)
					await _transaction.CommitAsync();
			}
			catch (Exception)
			{
				_transaction?.Rollback();
				throw;
			}
		}

		public async Task<int> CommitTransactionSaveChangesAsync()
		{
			try
			{
				var res = await _dbContext.SaveChangesAsync();

				if (_transaction != null)
					await _transaction.CommitAsync();

				return res;
			}
			catch (Exception)
			{
				_transaction?.Rollback();
				throw;
			}
		}

		public int SaveChanges()
		{
			try
			{
				var res = _dbContext.SaveChanges();
				_transaction?.Commit();

				return res;
			}
			catch (Exception)
			{
				_transaction?.Rollback();
				throw;
			}
		}
	}

}
