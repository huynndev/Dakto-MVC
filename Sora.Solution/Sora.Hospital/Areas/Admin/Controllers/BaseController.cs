using Sora.Hospital.Infrastructure.Security;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public static readonly log4net.ILog log
               = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}