using Sora.Common.Constants;
using Sora.Hospital.Infrastructure.Security;
using Sora.Hospital.Infrastructure.VirtualObject;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
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
            ViewBag.ActiveMenu = "config-company";

            CompanyViewModel companyViewModel = _companyService.GetCompanyInfo();

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];

            return View(companyViewModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(CompanyViewModel model, HttpPostedFileBase avatar)
        {
            //JSonResponse result = new JSonResponse();

            try
            {
                if (avatar != null && avatar.ContentLength > 0)
                {
                    // Kiểm tra định dạng file
                    string savePath = Server.MapPath(Constants.PATH_IMAGE_COMPANY);
                    string fileExtension = Path.GetExtension(avatar.FileName);
                    Guid fileName = Guid.NewGuid();
                    if (Constants.ACCEPT_FILE_IMAGE.Exists(x => x.EndsWith(fileExtension.ToLower())))
                    {
                        var filePath = Path.Combine(savePath, fileName + fileExtension);
                        avatar.SaveAs(filePath);
                        model.CSCompanyAvatar = string.Format("{0}{1}", fileName, fileExtension);
                    }
                }

                _companyService.Update(model);

                TempData["Success"] = true;
                TempData["Message"] = "Cập nhật thành công.";
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Cập nhật thất bại, Vui lòng thử lại!";
            }

            return RedirectToAction("Index"); ;
        }
    }
}