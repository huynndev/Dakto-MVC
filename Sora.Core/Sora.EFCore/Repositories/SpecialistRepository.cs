using Sora.EFCore.Infrastructure;
using Sora.Entites.ME;

namespace Sora.EFCore.Repositories
{
    public interface ISpecialistRepository : IRepository<MESpecialist>
    {
    }

    public class SpecialistRepository : RepositoryBase<MESpecialist>, ISpecialistRepository
    {
        public SpecialistRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
