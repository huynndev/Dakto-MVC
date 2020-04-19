using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/appointment")]
    public class AppointmentController : ApiControllerBase
    {
        #region Properties
        private readonly IAppointmentService _appointmentService;
        #endregion

        #region Constructor
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        #endregion

        #region Public methods
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody] CreateAppointmentDto dto)
        {
            _appointmentService.Create(dto);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        #endregion
    }
}
