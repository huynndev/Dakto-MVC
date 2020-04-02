using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/specialist")]
    public class SpecialistController : ApiControllerBase
    {
        #region Properties
        private readonly ISpecialistService _specialistService;
        private readonly ISpecialistTypeService _specialistTypeService;
        #endregion

        #region Constructor
        public SpecialistController(ISpecialistService specialistService, ISpecialistTypeService specialistTypeService)
        {
            _specialistService = specialistService;
            _specialistTypeService = specialistTypeService;
        }
        #endregion

        #region Public method
        /// <summary>
        /// Lấy danh sách chuyên khoa
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        public HttpResponseMessage GetAll()
        {
            var result = _specialistTypeService.GetAllDetail();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết chuyên khoa
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = _specialistService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy danh sách loại chuyên khoa
        /// </summary>
        [HttpGet]
        [Route("types")]
        public HttpResponseMessage GetTypes()
        {
            var result = _specialistTypeService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

    }
}
