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

    [RoutePrefix("Api/Company")]
    public class CompanyController : ApiControllerBase
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService) : base()
        {
            this._companyService = companyService;
        }

        [HttpGet]
        [Route("GetCompanyInfo")]
        public HttpResponseMessage GetCompanyInfo()
        {
            var comparnyInfo = _companyService.GetCompanyInfo();
            return Request.CreateResponse(HttpStatusCode.OK, comparnyInfo);
        }
    }
}
