using Sora.EFCore.Infrastructure;
using Sora.Entites.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.EFCore.Repositories
{
    public interface ICompanyRepository : IRepository<CSCompany>
    {
    }

    public class CompanyRepository : RepositoryBase<CSCompany>, ICompanyRepository
    {
        public CompanyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
