using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductsController : ApiControllerBase
    {
        #region Properties
        private readonly IProductService _productService;
        private readonly IProductGroupService _productGroupService;
        #endregion

        #region Constructor
        public ProductsController(IProductService productService, IProductGroupService productGroupService)
        {
            _productService = productService;
            _productGroupService = productGroupService;
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Lấy danh sách nhóm sản phẩm
        /// </summary>
        [HttpGet]
        [Route("groups")]
        public HttpResponseMessage Groups()
        {
            var result = _productGroupService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết sản phẩm
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get([FromUri] int id)
        {
            var result = _productService.Get(id).FullUrlImageProduct();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lọc danh sách sản phẩm
        /// </summary>
        [HttpGet]
        [Route("filter/{page}/{pageSize}")]
        public HttpResponseMessage Filter([FromUri] int page, [FromUri] int pageSize, int? groupId, string productType, string search)
        {
            var result = _productService.Filter(page, pageSize, groupId, true, productType, search);
            result.Items = result.Items.Select(x => x.FullUrlImageProduct()).ToArray();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
    }
}
