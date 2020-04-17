using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class DocumentTypesController : BaseController
    {
        #region Properties
        private readonly IDocumentService _documentService;
        #endregion

        #region Constructor
        public DocumentTypesController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        #endregion

        #region Public methods
        // GET: Admin/DocumentTypes
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "document-type-index";
            var result = _documentService.GetAllDocumentType();
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];

            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "document-type-index";
            var result = _documentService.GetDocumentType(id);
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "document-type-index";
            DocumentTypeViewModel specialist = new DocumentTypeViewModel();
            return View("Detail", specialist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(DocumentTypeViewModel dto)
        {
            ViewBag.ActiveMenu = "document-type-index";
            var isCreate = true;
            try
            {
                if (!dto.DocumentTypeID.HasValue)
                {
                    _documentService.CreateDocumentType(dto);
                    TempData["Message"] = "Tạo mới loại văn bản thành công.";
                }
                else
                {
                    isCreate = false;
                    _documentService.UpdateDocumentType(dto);
                    TempData["Message"] = "Cập nhật loại văn bản thành công.";
                }
                TempData["Success"] = true;
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới loại văn bản thất bại, vui lòng thử lại." : "Cập nhật loại văn bản thất bại, vui lòng thử lại.";
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _documentService.DeleteDocumentType(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa loại văn bản thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = true;
                TempData["Message"] = "Xóa loại văn bản thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}