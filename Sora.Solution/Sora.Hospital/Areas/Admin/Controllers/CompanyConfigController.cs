using Sora.Hospital.Infrastructure.VirtualObject;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    public class CompanyConfigController : BaseController
    {
        private ICompanyService _companyService;

        public CompanyConfigController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: Admin/CompanyConfig
        public ActionResult Index()
        {
            // get group setting by Id

            CompanyViewModel companyViewModel = new CompanyViewModel();
            try
            {
                companyViewModel = _companyService.GetCompanyInfo();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message + "\n" + ex.StackTrace, User.FullName);
                JSonResponse result = new JSonResponse();
                result.status = JSonResponse.Status.Error;
                result.data = ex.Source;
                result.message = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return View(companyViewModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(CompanyViewModel model)
        {
            JSonResponse result = new JSonResponse();

            try
            {
                _companyService.Update(model);

                result.status = JSonResponse.Status.Success;
                result.data = model;
                result.message = "Update " + model.CSCompanyName + " thành công";
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message + "\n" + ex.StackTrace, User.FullName);
                result.status = JSonResponse.Status.Error;
                result.data = ex.Source;
                result.message = ex.Message;
            }

            return RedirectToAction("Index"); ;
        }
    }
}