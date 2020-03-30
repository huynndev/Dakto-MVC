using Sora.Common.CommonObjects;
using Sora.Common.Constants;
using Sora.Common.Extensions;
using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
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
                Items = doctors.Select(x => SetUrlImageDoctor(x)).ToArray(),
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
            var result = SetUrlImageDoctor(_doctorService.Get(id));
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region Private method
        private DoctorViewModel SetUrlImageDoctor(DoctorViewModel dto)
        {
            dto.MEDoctorPicture = dto.MEDoctorPicture.IsNullOrWhiteSpace()
                ? Constants.PATH_IMAGE.GenerateFullUrl("noavatar.gif")
                : Constants.PATH_IMAGE_DOCTOR.GenerateFullUrl(dto.MEDoctorPicture);
            return dto;
        }
        #endregion
    }
}
