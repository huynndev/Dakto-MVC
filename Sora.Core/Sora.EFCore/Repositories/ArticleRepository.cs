using Sora.EFCore.Infrastructure;
using Sora.Entites.IC;
using System.Collections.Generic;

namespace Sora.EFCore.Repositories
{
    public interface IArticleRepository : IRepository<ICArticle>
    {

    }

    public class ArticleRepository : RepositoryBase<ICArticle>, IArticleRepository
    {
        public ArticleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}