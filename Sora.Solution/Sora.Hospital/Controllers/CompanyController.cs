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

    [RoutePrefix("api/company")]
    public class CompanyController : ApiControllerBase
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService) : base()
        {
            this._companyService = companyService;
        }

        /// <summary>
        /// Lấy thông tin công ty
        /// </summary>
        [HttpGet]
        [Route("get-info")]
        public HttpResponseMessage GetCompanyInfo()
        {
            var comparnyInfo = _companyService.GetCompanyInfo().FullUrlImageCompany();
            return Request.CreateResponse(HttpStatusCode.OK, comparnyInfo);
        }
    }
}
