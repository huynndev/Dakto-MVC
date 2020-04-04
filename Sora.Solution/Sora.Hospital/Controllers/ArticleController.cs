using Sora.Common.CommonObjects;
using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/article")]
    public class ArticleController : ApiControllerBase
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService)
            : base()
        {
            this._articleService = articleService;
        }

        /// <summary>
        /// Lấy chi tiết bài viết
        /// </summary>
        [HttpGet]
        [Route("get/{articleId}")]
        public HttpResponseMessage GetArticleByID(int articleId)
        {
            var category = _articleService.GetArticleByID(articleId);
            if (category == null)
                Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        /// <summary>
        /// Lấy những bài viết thuộc danh mục tin tức
        /// </summary>
        [HttpGet]
        [Route("get-by-category/{categoryId}/{page}/{pageSize}")]
        public HttpResponseMessage GetArticleByCategoryID(int categoryId, int page, int pageSize, string sortOption = "")
        {
            int totalRow = 0;
            var articles = _articleService.GetArticleByCategoryID(categoryId, page, pageSize, sortOption, out totalRow).Where(x=>x.ICArticleDate <= DateTime.Now);
            var paginationSet = new PagedResult<ArticleViewModel>()
            {
                Items = articles.ToArray(),
                PageIndex = page,
                TotalCount = articles.Count(),
                PageSize = pageSize,
                TotalPages = articles.Count() % pageSize == 0 ? articles.Count() / pageSize : (articles.Count() / pageSize) + 1
            };
            return Request.CreateResponse(HttpStatusCode.OK, paginationSet);
        }
    }
}
