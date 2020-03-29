using Sora.Services.Abstractions;
using System.Linq;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
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