using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("Api/ArticleCategory")]
    public class ArticleCategoryController : ApiControllerBase
    {
        private IArticleCategoryService _articleCategoryService;

        public ArticleCategoryController(IArticleCategoryService categoryService)
            : base()
        {
            this._articleCategoryService = categoryService;
        }

        [HttpGet]
        [Route("GetCategoryByID")]
        public HttpResponseMessage GetCategoryByID(int categoryID)
        {
            var category = _articleCategoryService.GetCategoryByID(categoryID);
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public HttpResponseMessage GetAllCategory()
        {
            var categories = _articleCategoryService.GetAllCategory();
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }
    }
}
