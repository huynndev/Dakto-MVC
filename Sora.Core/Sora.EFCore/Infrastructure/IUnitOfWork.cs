using Sora.Entites.Interfaces;

namespace Sora.EFCore.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Auditable;

        void Commit();

        int ExecuteSqlCommand(string sql, params object[] parameters);

        void CreateTransaction();

        void Save();

        void Rollback();
    }
}
