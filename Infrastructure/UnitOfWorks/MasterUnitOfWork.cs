using Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
	public class MasterUnitOfWork : UnitOfWork<AppDbContext>, IMasterUnitOfWork
	{
		public MasterUnitOfWork(AppDbContext dbContext) : base(dbContext)
		{
		}
	}
}
