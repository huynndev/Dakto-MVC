using Sora.EFCore.Infrastructure;
using Sora.Entites.ME;

namespace Sora.EFCore.Repositories
{
    public interface IDoctorRepository : IRepository<MEDoctor>
    {
    }
    public class DoctorRepository : RepositoryBase<MEDoctor>, IDoctorRepository
    {
        public DoctorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
