using Sora.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.EFCore.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        HospitalDbContext Init();
    }
}
