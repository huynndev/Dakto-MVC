using Sora.EFCore.Infrastructure;
using Sora.Entites.IC;
using System.Collections.Generic;

namespace Sora.EFCore.Repositories
{
    public interface IArticleCategoryRepository : IRepository<ICArticleCategory>
    {
       
    }

    public class ArticleCategoryRepository : RepositoryBase<ICArticleCategory>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}