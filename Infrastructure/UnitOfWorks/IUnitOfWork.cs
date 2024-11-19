using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
	public interface IUnitOfWork<TDbContext>
	where TDbContext : DbContext
	{
		Task BeginTransactionAsync();

		Task<int> CommitTransactionSaveChangesAsync();

		Task<int> SaveChangesAsync();
	}
}
