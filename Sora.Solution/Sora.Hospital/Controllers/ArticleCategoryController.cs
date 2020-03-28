using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/article-category")]
    public class ArticleCategoryController : ApiControllerBase
    {
        private IArticleCategoryService _articleCategoryService;

        public ArticleCategoryController(IArticleCategoryService categoryService)
            : base()
        {
            this._articleCategoryService = categoryService;
        }

        /// <summary>
        /// Lấy chi tiết danh mục tin tức
        /// </summary>
        [HttpGet]
        [Route("get/{categoryId}")]
        public HttpResponseMessage GetCategoryByID(int categoryId)
        {
            var category = _articleCategoryService.GetCategoryByID(categoryId);
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        /// <summary>
        /// Lấy tất cả danh mục tin tức
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        public HttpResponseMessage GetAllCategory()
        {
            var categories = _articleCategoryService.GetAllCategory();
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }
    }
}
