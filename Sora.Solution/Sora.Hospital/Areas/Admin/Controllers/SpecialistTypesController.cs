using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class SpecialistTypesController : BaseController
    {
        #region Properties
        private readonly ISpecialistTypeService _specialistTypeService;
        #endregion

        #region Constructor
        public SpecialistTypesController(ISpecialistTypeService specialistTypeService)
        {
            _specialistTypeService = specialistTypeService;
        }
        #endregion

        #region Public method

        // GET: Admin/Specialists
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "specialist-type-index";
            var result = _specialistTypeService.GetAll();
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];

            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "specialist-type-index";
            var result = _specialistTypeService.Get(id);
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "specialist-type-index";
            SpecialistTypeViewModel specialist = new SpecialistTypeViewModel();
            return View("Detail", specialist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SpecialistTypeViewModel dto)
        {
            ViewBag.ActiveMenu = "specialist-type-index";
            var isCreate = true;
            try
            {
                if (!dto.MESpecialistTypeID.HasValue)
                {
                    _specialistTypeService.Create(dto);
                    TempData["Message"] = "Tạo mới loại chuyên khoa thành công.";
                }
                else
                {
                    isCreate = false;
                    _specialistTypeService.Update(dto);
                    TempData["Message"] = "Cập nhật loại chuyên khoa thành công.";
                }
                TempData["Success"] = true;
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới loại chuyên khoa thất bại, vui lòng thử lại." : "Cập nhật loại chuyên khoa thất bại, vui lòng thử lại.";
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _specialistTypeService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa loại chuyên khoa thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = true;
                TempData["Message"] = "Xóa loại chuyên khoa thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}