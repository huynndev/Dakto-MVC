using Sora.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.EFCore.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private HospitalDbContext dbContext;

        public HospitalDbContext Init()
        {
            return dbContext ?? (dbContext = new HospitalDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}