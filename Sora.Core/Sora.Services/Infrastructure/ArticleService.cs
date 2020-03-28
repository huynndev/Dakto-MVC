using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.IC;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Sora.Services.Infrastructure
{
    public class ArticleService : IArticleService
    {

        private IArticleRepository _articleRepository;
        private IUnitOfWork _unitOfWork;

        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            this._articleRepository = articleRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<ArticleViewModel> GetArticleByCategoryID(int categoryID, int page, int pageSize, string sortOption, out int totalRow, string search = null)
        {
            var articles = _articleRepository.GetMulti(o => o.FK_ICArticleCategoryID == categoryID).Where(x => string.IsNullOrWhiteSpace(search) || x.ICArticleTitle.ToLower().Contains(search.ToLower())).Skip(page - 1 * pageSize).Take(pageSize);
            if(categoryID == 0)
            {
                articles = _articleRepository.GetMulti(o => o.AAStatus == "Alive").Where(x => string.IsNullOrWhiteSpace(search) || x.ICArticleTitle.ToLower().Contains(search.ToLower())).Skip(page - 1 * pageSize).Take(pageSize);
            }
            switch (sortOption)
            {
                case "Category":
                    articles = articles.OrderByDescending(o => o.ICArticleCategory);
                    break;
                default:
                    articles = articles.OrderByDescending(o => o.ICArticleDate);
                    break;
            }
            totalRow = articles.Count();
            return articles.Select(o => ToArticleViewModel(o));
        }

        public ArticleViewModel GetArticleByID(int articleID)
        {
            var article = _articleRepository.GetSingleById(articleID);
            if (article == null)
                return null;

            return ToArticleViewModel(article);
        }

        public ArticleViewModel ToArticleViewModel(ICArticle article)
        {
            return new ArticleViewModel()
            {
                ICArticleID = article.ICArticleID,
                FK_ICArticleCategoryID = article.FK_ICArticleCategoryID,
                ICArticleFriendlyUrl = article.ICArticleFriendlyUrl,
                ICArticleTitle = article.ICArticleTitle,
                ICArticleDate = article.ICArticleDate,
                ICArticleShortContent = article.ICArticleShortContent,
                ICArticleContent = article.ICArticleContent,
                ICArticlePicture = article.ICArticlePicture,
                ICArticleMetaKeyword = article.ICArticleMetaKeyword,
                ICArticleMetaDesc = article.ICArticleMetaDesc,
                ICArticleTags = article.ICArticleTags,
                ICArticleIsShowHome = article.ICArticleIsShowHome ?? false
            };
        }

        public ICArticle ToArticle(ArticleViewModel article)
        {
            return new ICArticle()
            {
                ICArticleID = article.ICArticleID,
                FK_ICArticleCategoryID = article.FK_ICArticleCategoryID,
                ICArticleFriendlyUrl = article.ICArticleFriendlyUrl,
                ICArticleTitle = article.ICArticleTitle,
                ICArticleDate = article.ICArticleDate,
                ICArticleShortContent = article.ICArticleShortContent,
                ICArticleContent = article.ICArticleContent,
                ICArticlePicture = article.ICArticlePicture,
                ICArticleMetaKeyword = article.ICArticleMetaKeyword,
                ICArticleMetaDesc = article.ICArticleMetaDesc,
                ICArticleTags = article.ICArticleTags,
                ICArticleIsShowHome = article.ICArticleIsShowHome
            };
        }

        public void Add(ArticleViewModel category)
        {
            _articleRepository.Add(ToArticle(category));
        }

        public void CreateArticle(ArticleViewModel obj)
        {
            Add(obj);
            Save();
        }

        public void Update(ArticleViewModel obj)
        {
            _articleRepository.Update(ToArticle(obj));
            Save();
        }

        public void Delete(ArticleViewModel obj)
        {
            ICArticle article = ToArticle(obj);
            _articleRepository.DeleteSoft(article);
            Save();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
