using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/script")]
    public class ScriptController : ApiControllerBase
    {
        #region Properties
        private readonly IScriptService _scriptService;
        #endregion

        #region Constructor
        public ScriptController(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }
        #endregion

        #region Public method
        [HttpPost]
        [Route("run")]
        public HttpResponseMessage RunScript([FromBody] ScriptDto dto)
        {
            _scriptService.Run(dto.Script);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        #endregion
    }
}
