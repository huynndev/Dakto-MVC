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
    [RoutePrefix("api/commune")]
    public class CommuneController : ApiControllerBase
    {
        #region Properties
        private readonly ICommuneService _communeService;
        #endregion

        #region Constructor
        public CommuneController(ICommuneService communeService)
        {
            _communeService = communeService;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Lấy danh sách tuyến xã
        /// </summary>
        [HttpGet]
        [Route("filter/{page}/{pageSize}")]
        public HttpResponseMessage Filter(int page, int pageSize, string search)
        {
            var result = _communeService.Filter(page, pageSize, search);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết tuyến xã
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Detail(int id)
        {
            var result = _communeService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
    }
}
