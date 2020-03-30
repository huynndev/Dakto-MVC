using Sora.EFCore.Infrastructure;
using Sora.Entites.ME;

namespace Sora.EFCore.Repositories
{
    public interface IDoctorSpecialistRepository : IRepository<MEDoctorSpecialist> { }
    public class DoctorSpecialistRepository : RepositoryBase<MEDoctorSpecialist>, IDoctorSpecialistRepository
    {
        public DoctorSpecialistRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
