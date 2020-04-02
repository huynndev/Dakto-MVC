using Sora.Common.CommonObjects;
using Sora.Common.Constants;
using Sora.Common.Extensions;
using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.Infrastructure.Helpers;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/doctor")]
    public class DoctorController : ApiControllerBase
    {
        #region Properties
        private readonly IDoctorService _doctorService;
        #endregion

        #region Constructor
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        #endregion

        #region Public method
        /// <summary>
        /// Lọc danh sách hồ sơ bác sĩ
        /// </summary>
        [HttpGet]
        [Route("filter/{specialistId}/{page}/{pageSize}")]
        public HttpResponseMessage GetAll(int specialistId, int page, int pageSize)
        {
            var total = 0;
            var doctors = _doctorService.Filter(page, pageSize, out total, new int[1] { specialistId });
            var result = new PagedResult<DoctorViewModel>
            {
                Items = doctors.Select(x => x.FullUrlImageDoctor()).ToArray(),
                PageIndex = page,
                TotalCount = total,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize)
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết hồ sơ bác sĩ
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result =_doctorService.Get(id).FullUrlImageDoctor();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion
    }
}
