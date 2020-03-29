using Sora.EFCore.Infrastructure;
using Sora.Entites.AD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.EFCore.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {

    }

    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
