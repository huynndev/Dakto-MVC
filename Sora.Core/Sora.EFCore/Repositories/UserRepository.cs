using Sora.EFCore.Infrastructure;
using Sora.Entites.AD;
using Sora.Entites.IC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.EFCore.Repositories
{
    public interface IUserRepository : IRepository<ADUser>
    {

    }

    public class UserRepository : RepositoryBase<ADUser>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
