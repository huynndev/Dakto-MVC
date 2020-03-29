using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using System.Linq;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class SALogController : BaseController
    {
        private readonly ILogService _logService;

        public SALogController(ILogService logService)
        {
            _logService = logService;
        }

        // GET: Admin/Log
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "error-log";
            var result = _logService.GetAll();
            return View(result.ToList());
        }
    }
}