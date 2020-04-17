using Sora.Common.Constants;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class DocumentsController : BaseController
    {
        #region Properties
        private readonly IDocumentService _documentService;
        #endregion

        #region Constructor
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        #endregion

        #region Public methods
        // GET: Admin/Documents
        public ActionResult Index(int? type, int page = 1, int pageSize = 10, string search = "")
        {
            ViewBag.ActiveMenu = "document-index";
            var result = _documentService.FilterDocument(page - 1, pageSize, type, search);
            ViewData["Types"] = _documentService.GetAllDocumentType();

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            ViewBag.Type = type;
            ViewBag.Search = search;

            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "document-index";
            ViewData["Types"] = _documentService.GetAllDocumentType();
            var document = _documentService.GetDocument(id);
            return View(document);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "document-index";
            ViewData["Types"] = _documentService.GetAllDocumentType();
            DocumentViewModel document = new DocumentViewModel();
            return View("Detail", document);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(DocumentViewModel dto, HttpPostedFileBase file)
        {
            ViewBag.ActiveMenu = "document-index";
            var isCreate = true;
            if (file != null && file.ContentLength > 0)
            {
                // Kiểm tra định dạng file
                string savePath = Server.MapPath(Constants.PATH_IMAGE_DOCUMENT);
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = file.FileName.Replace(" ", "");
                if (Constants.ACCEPT_FILE_IMAGE.Exists(x => x.EndsWith(fileExtension.ToLower())))
                {
                    var filePath = Path.Combine(savePath, fileName);
                    file.SaveAs(filePath);
                    dto.File = string.Format("{0}", fileName);
                }
            }
            try
            {
                if (!dto.DocumentID.HasValue)
                {
                    _documentService.CreateDocument(dto);
                    TempData["Message"] = "Tạo mới văn bản thành công.";
                }
                else
                {
                    isCreate = false;
                    _documentService.UpdateDocument(dto);
                    TempData["Message"] = "Cập nhật văn bản thành công.";
                }

                TempData["Success"] = true;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới văn bản thất bại, vui lòng thử lại." : "Cập nhật văn bản thất bại, vui lòng thử lại.";
                ViewData["Types"] = _documentService.GetAllDocumentType();
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _documentService.DeleteDocument(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa văn bản thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa văn bản thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}