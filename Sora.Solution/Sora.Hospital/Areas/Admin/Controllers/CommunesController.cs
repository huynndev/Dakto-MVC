using Sora.Common.Constants;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class CommunesController : BaseController
    {
        #region Properties
        private readonly ICommuneService _communeService;
        #endregion

        #region Constructor
        public CommunesController(ICommuneService communeService)
        {
            _communeService = communeService;
        }
        #endregion

        #region Public methods
        // GET: Admin/Communes
        public ActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            ViewBag.ActiveMenu = "commune-index";

            var result = _communeService.Filter(page - 1, pageSize, search);

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            ViewBag.Search = search;
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "commune-index";
            CommuneViewModel commune = new CommuneViewModel();

            return View("Detail", commune);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "commune-index";
            var commune = _communeService.Get(id);

            return View(commune);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CommuneViewModel dto, HttpPostedFileBase avatar)
        {
            ViewBag.ActiveMenu = "commune-index";
            var isCreate = true;
            if (avatar != null && avatar.ContentLength > 0)
            {
                // Kiểm tra định dạng file
                string savePath = Server.MapPath(Constants.PATH_IMAGE_COMMUNE);
                string fileExtension = Path.GetExtension(avatar.FileName);
                Guid fileName = Guid.NewGuid();
                if (Constants.ACCEPT_FILE_IMAGE.Exists(x => x.EndsWith(fileExtension.ToLower())))
                {
                    var filePath = Path.Combine(savePath, fileName + fileExtension);
                    avatar.SaveAs(filePath);
                    dto.Image = string.Format("{0}{1}", fileName, fileExtension);
                }
            }
            try
            {
                if (!dto.CommuneID.HasValue)
                {
                    _communeService.Create(dto);
                    TempData["Message"] = "Tạo mới tuyến xã thành công.";
                }
                else
                {
                    isCreate = false;
                    _communeService.Update(dto);
                    TempData["Message"] = "Cập nhật tuyến xã thành công.";
                }

                TempData["Success"] = true;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới tuyến xã thất bại, vui lòng thử lại." : "Cập nhật tuyến xã thất bại, vui lòng thử lại.";
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _communeService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa tuyến xã thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa tuyến xã thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}