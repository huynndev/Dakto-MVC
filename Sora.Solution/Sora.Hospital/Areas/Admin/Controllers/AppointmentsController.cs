using Sora.Common.Enums;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class AppointmentsController : BaseController
    {
        #region Properties
        private readonly IAppointmentService _appointmentService;
        private readonly ISpecialistService _specialistService;
        #endregion

        #region Constructor
        public AppointmentsController(IAppointmentService appointmentService, ISpecialistService specialistService)
        {
            _appointmentService = appointmentService;
            _specialistService = specialistService;
        }
        #endregion

        #region Public methods
        // GET: Admin/Appointments
        public ActionResult Index(int page = 1, int pageSize = 10, int? specialistId = null, string search = "")
        {
            ViewBag.ActiveMenu = "appointment-index";
            var result = _appointmentService.Filter(page - 1, pageSize, specialistId, search);

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            ViewBag.Specialist = specialistId;
            ViewBag.Search = search;
            ViewData["Specialists"] = _specialistService.GetAll();

            return View(result);
        }
        
        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "appointment-index";
            var appointment = _appointmentService.Get(id);
            return View(appointment);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "appointment-index";
            AppointmentViewModel appointment = new AppointmentViewModel();
            return View("Detail", appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(AppointmentViewModel dto)
        {
            ViewBag.ActiveMenu = "appointment-index";
            var isCreate = true;
            try
            {
                if (!dto.Id.HasValue)
                {
                    _appointmentService.Create(dto);
                    TempData["Message"] = "Tạo mới đăng ký khám thành công.";
                }
                else
                {
                    isCreate = false;
                    _appointmentService.Update(dto);
                    TempData["Message"] = "Cập nhật đăng ký khám thành công.";
                }

                TempData["Success"] = true;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới đăng ký khám thất bại, vui lòng thử lại." : "Cập nhật đăng ký khám thất bại, vui lòng thử lại.";
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _appointmentService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa đăng ký khám thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa đăng ký khám thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {

            ViewBag.ActiveMenu = "appointment-index";
            try
            {
                _appointmentService.UpdateStatus(id, status);
                TempData["Message"] = "Cập nhật trạng thái thông tin đăng ký khám thành công.";
                TempData["Success"] = true;
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = "Cập nhật trạng thái thông tin đăng ký khám thất bại, vui lòng thử lại.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Calendar()
        {
            ViewBag.ActiveMenu = "appointment-calendar";
            var result = _appointmentService.GetAll();
            return View(result);
        }
        #endregion
    }
}