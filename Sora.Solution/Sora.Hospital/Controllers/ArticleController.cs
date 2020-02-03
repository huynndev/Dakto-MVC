using Sora.Entites.CS;
using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("Api/Article")]
    public class ArticleController : ApiControllerBase
    {
        private IArticleService _articleService;

        public ArticleController(IArticleService articleService)
            : base()
        {
            this._articleService = articleService;
        }

        [HttpGet]
        [Route("GetArticleByID")]
        public HttpResponseMessage GetArticleByID(int articleID)
        {
            var category = _articleService.GetArticleByID(articleID);
            if (category == null)
                Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        [HttpGet]
        [Route("GetArticleByCategoryID")]
        public HttpResponseMessage GetArticleByCategoryID(int categoryID, int page = 1, int pageSize = 20, string sortOption = "")
        {
            int totalRow = 0;
            var articles = _articleService.GetArticleByCategoryID(categoryID, page, pageSize, sortOption, out totalRow);
            var paginationSet = new PaginationSet<ArticleViewModel>()
            {
                Items = articles,
                PageIndex = page,
                TotalRow = totalRow,
                PageSize = pageSize
            };
            return Request.CreateResponse(HttpStatusCode.OK, paginationSet);
        }
    }
}
