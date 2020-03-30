using Sora.EFCore.Infrastructure;
using Sora.Entites.AD;

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
