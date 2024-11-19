using Infrastructure.DbContexts;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
	public interface IMasterUnitOfWork: IUnitOfWork<AppDbContext>
	{
	}
}
