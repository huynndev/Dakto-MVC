using Sora.Common.CommonObjects;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class SpecialistsController : BaseController
    {
        #region Properties
        private readonly ISpecialistService _specialistService;
        private readonly ISpecialistTypeService _specialistTypeService;
        private readonly IDoctorService _doctorService;
        #endregion

        #region Constructor
        public SpecialistsController(ISpecialistService specialistService, IDoctorService doctorService, ISpecialistTypeService specialistTypeService)
        {
            _specialistService = specialistService;
            _doctorService = doctorService;
            _specialistTypeService = specialistTypeService;
        }
        #endregion

        #region Public method

        // GET: Admin/Specialists
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "specialist-index";
            var result = _specialistService.GetAll();
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];

            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "specialist-index";
            var result = _specialistService.Get(id);
            ViewData["Doctors"] = _doctorService.GetAll().ToList();
            ViewData["Types"] = _specialistTypeService.GetAll().ToList();
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "specialist-index";
            SpecialistViewModel specialist = new SpecialistViewModel();
            ViewData["Doctors"] = _doctorService.GetAll().ToList();
            ViewData["Types"] = _specialistTypeService.GetAll().ToList();
            return View("Detail", specialist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SpecialistViewModel dto)
        {
            ViewBag.ActiveMenu = "specialist-index";
            var isCreate = true;
            try
            {
                if (!dto.MESpecialistID.HasValue)
                {
                    _specialistService.Create(dto);
                    TempData["Message"] = "Tạo mới chuyên khoa thành công.";
                }
                else
                {
                    isCreate = false;
                    _specialistService.Update(dto);
                    TempData["Message"] = "Cập nhật chuyên khoa thành công.";
                }
                TempData["Success"] = true;
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới chuyên khoa thất bại, vui lòng thử lại." : "Cập nhật chuyên khoa thất bại, vui lòng thử lại.";
                ViewData["Doctors"] = _doctorService.GetAll().ToList();
                ViewData["Types"] = _specialistTypeService.GetAll().ToList();
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _specialistService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa chuyên khoa thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa chuyên khoa thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
}