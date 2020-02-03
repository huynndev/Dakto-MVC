using Sora.Hospital.Infrastructure.Security;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class ManagerController : BaseController
    {
        // GET: Admin/Manager
        public ActionResult Index()
        {
            DashboardViewModel data = new DashboardViewModel()
            {
                NumOfContact  = 10,
                NumOfCustomer = 15,
                NumOfExpert = 100,
                NumOfModel = 8
            }; 
            return View(data);
        }
    }
}