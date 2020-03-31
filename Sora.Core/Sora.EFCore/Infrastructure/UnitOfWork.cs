using Sora.EFCore.Contexts;

namespace Sora.EFCore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;

        private HospitalDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public HospitalDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters) => DbContext.Database.ExecuteSqlCommand(sql, parameters);
    }
}
