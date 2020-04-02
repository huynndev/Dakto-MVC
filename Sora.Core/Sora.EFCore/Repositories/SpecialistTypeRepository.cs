using Sora.EFCore.Infrastructure;
using Sora.Entites.ME;

namespace Sora.EFCore.Repositories
{
    public interface ISpecialistTypeRepository : IRepository<MESpecialistType>
    {
    }
    public class SpecialistTypeRepository : RepositoryBase<MESpecialistType>, ISpecialistTypeRepository
    {
        public SpecialistTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
