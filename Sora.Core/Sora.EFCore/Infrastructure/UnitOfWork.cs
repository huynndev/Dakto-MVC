using Sora.EFCore.Contexts;
using Sora.Entites.Interfaces;
using System;
using System.Collections.Generic;

namespace Sora.EFCore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;

        private HospitalDbContext dbContext;

        private Dictionary<Type, object> repositories;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Auditable
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new RepositoryBase<TEntity>(dbFactory);

            }

            return (IRepository<TEntity>)repositories[type];
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
