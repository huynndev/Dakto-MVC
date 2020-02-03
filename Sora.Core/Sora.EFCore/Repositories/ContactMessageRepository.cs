using Sora.EFCore.Infrastructure;
using Sora.Entites.IC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.EFCore.Repositories
{
    public interface IContactMessageRepository : IRepository<ICContactMessage>
    {

    }

    public class ContactMessageRepository : RepositoryBase<ICContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
