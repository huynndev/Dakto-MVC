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
    public class ProductsController : BaseController
    {
        #region Properties 
        private readonly IProductService _productService;
        private readonly IProductGroupService _productGroupService;
        private readonly ISpecialistService _specialistService;
        #endregion

        #region Constructor
        public ProductsController(IProductService productService, IProductGroupService productGroupService, ISpecialistService specialistService)
        {
            _productService = productService;
            _productGroupService = productGroupService;
            _specialistService = specialistService;
        }
        #endregion

        #region Public methods
        // GET: Admin/Products
        public ActionResult Index(int? page, int? pageSize, int? groupId, string search)
        {
            ViewBag.ActiveMenu = "product-index";
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;
            var result = _productService.Filter(page.GetValueOrDefault() - 1, pageSize.GetValueOrDefault(), groupId, null, search);
            ViewData["Groups"] = _productGroupService.GetAll();

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "product-index";
            ViewData["Groups"] = _productGroupService.GetAll();
            ViewData["Specialists"] = _specialistService.GetAll();
            ViewData["Products"] = _productService.GetAll(false);
            var product = _productService.Get(id);
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "product-index";
            ViewData["Groups"] = _productGroupService.GetAll();
            ViewData["Specialists"] = _specialistService.GetAll();
            ViewData["Products"] = _productService.GetAll(false);
            ProductViewModel product = new ProductViewModel();
            return View("Detail", product);
        }

        public ActionResult Save(ProductViewModel dto, HttpPostedFileBase avatar)
        {
            ViewBag.ActiveMenu = "product-index";
            var isCreate = true;
            if (avatar != null && avatar.ContentLength > 0)
            {
                // Kiểm tra định dạng file
                string savePath = Server.MapPath(Constants.PATH_IMAGE_PRODUCT);
                string fileExtension = Path.GetExtension(avatar.FileName);
                Guid fileName = Guid.NewGuid();
                if (Constants.ACCEPT_FILE_IMAGE.Exists(x => x.EndsWith(fileExtension.ToLower())))
                {
                    var filePath = Path.Combine(savePath, fileName + fileExtension);
                    avatar.SaveAs(filePath);
                    dto.ICProductPicture = string.Format("{0}{1}", fileName, fileExtension);
                }
            }
            try
            {
                if (!dto.ICProductID.HasValue)
                {
                    _productService.Create(dto);
                    TempData["Message"] = "Tạo mới sản phẩm thành công.";
                }
                else
                {
                    isCreate = false;
                    _productService.Update(dto);
                    TempData["Message"] = "Cập nhật sản phẩm thành công.";
                }

                TempData["Success"] = true;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới sản phẩm thất bại, vui lòng thử lại." : "Cập nhật sản phẩm thất bại, vui lòng thử lại."; ;
                ViewData["Groups"] = _productGroupService.GetAll();
                ViewData["Specialists"] = _specialistService.GetAll();
                ViewData["Products"] = _productService.GetAll(false);
                return View("Detail", dto);
            }
        }
        #endregion
    }
}