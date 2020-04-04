using Sora.Common.CommonObjects;
using Sora.Common.Constants;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class DoctorsController : BaseController
    {
        #region Properties
        private readonly IDoctorService _doctorService;
        private readonly ISpecialistService _specialistService;
        #endregion

        #region Constructor
        public DoctorsController(IDoctorService doctorService, ISpecialistService specialistService)
        {
            _doctorService = doctorService;
            _specialistService = specialistService;
        }
        #endregion

        #region Public method
        // GET: Admin/Doctors
        public ActionResult Index(int? page, int? pageSize, string search, int[] specialistIds)
        {
            ViewBag.ActiveMenu = "doctor-index";
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;
            int totalRecords = 0;
            var doctors = _doctorService.Filter(page.GetValueOrDefault(), pageSize.GetValueOrDefault(), out totalRecords, specialistIds, search);
            var result = new PagedResult<DoctorViewModel>
            {
                Items = doctors.ToArray(),
                PageIndex = page.GetValueOrDefault() - 1,
                TotalCount = totalRecords,
                PageSize = pageSize.GetValueOrDefault(),
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            ViewData["Specialists"] = _specialistService.GetAll().Select(x => new SelectionData { Value = x.MESpecialistID.ToString(), Text = x.MESpecialistName.ToString() }).ToList();
            ViewData["SpecialistsChoosed"] = specialistIds;

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];

            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "doctor-index";
            DoctorViewModel doctor = new DoctorViewModel();
            ViewData["Specialists"] = _specialistService.GetAll().Select(x => new SelectionData { Value = x.MESpecialistID.ToString(), Text = x.MESpecialistName.ToString() }).ToList();
            return View("Detail", doctor);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "doctor-index";
            var doctor = _doctorService.Get(id);
            ViewData["Specialists"] = _specialistService.GetAll().Select(x => new SelectionData { Value = x.MESpecialistID.ToString(), Text = x.MESpecialistName.ToString() }).ToList();
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(DoctorViewModel dto, HttpPostedFileBase avatar)
        {
            var isCreate = true;
            if (avatar != null && avatar.ContentLength > 0)
            {
                // Kiểm tra định dạng file
                string savePath = Server.MapPath(Constants.PATH_IMAGE_DOCTOR);
                string fileExtension = Path.GetExtension(avatar.FileName);
                Guid fileName = Guid.NewGuid();
                if (Constants.ACCEPT_FILE_IMAGE.Exists(x => x.EndsWith(fileExtension.ToLower())))
                {
                    var filePath = Path.Combine(savePath, fileName + fileExtension);
                    avatar.SaveAs(filePath);
                    dto.MEDoctorPicture = string.Format("{0}{1}", fileName, fileExtension);
                }
            }
            try
            {
                if (!dto.MEDoctorID.HasValue)
                {
                    _doctorService.Create(dto);
                    TempData["Message"] = "Tạo mới hồ sơ bác sĩ thành công.";
                }
                else
                {
                    isCreate = false;
                    _doctorService.Update(dto);
                    TempData["Message"] = "Cập nhật hồ sơ bác sĩ thành công.";
                }

                TempData["Success"] = true;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới hồ sơ bác sĩ thất bại, vui lòng thử lại." : "Cập nhật hồ sơ bác sĩ thất bại, vui lòng thử lại."; ;
                ViewData["Specialists"] = _specialistService.GetAll().Select(x => new SelectionData { Value = x.MESpecialistID.ToString(), Text = x.MESpecialistName.ToString() }).ToList();

                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _doctorService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa hồ sơ bác sĩ thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa hồ sơ bác sĩ thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
}