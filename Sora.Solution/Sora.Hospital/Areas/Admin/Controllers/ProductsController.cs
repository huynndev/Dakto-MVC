using Sora.Common.Constants;
using Sora.Common.Enums;
using Sora.Common.Extensions;
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
        public ActionResult Index(int? page, int? pageSize, int? groupId, string search, string productType)
        {
            ViewBag.ActiveMenu = "product-index";
            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;
            var result = _productService.Filter(page.GetValueOrDefault() - 1, pageSize.GetValueOrDefault(), groupId, null, productType, search);
            ViewData["Groups"] = _productGroupService.GetAll();

            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            ViewBag.ProductType = productType;
            ViewBag.Search = search;

            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "product-index";
            ViewData["Groups"] = _productGroupService.GetAll();
            ViewData["Specialists"] = _specialistService.GetAll();
            ViewData["Products"] = _productService.GetAll();
            var product = _productService.Get(id);
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "product-index";
            ViewData["Groups"] = _productGroupService.GetAll();
            ViewData["Specialists"] = _specialistService.GetAll();
            ViewData["Products"] = _productService.GetAll();
            ProductViewModel product = new ProductViewModel();
            return View("Detail", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                ViewData["Products"] = _productService.GetAll();
                return View("Detail", dto);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa sản phẩm thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa sản phẩm thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public JavaScriptResult GetProducts()
        {
            var products = _productService.GetAll();
            string jsContent = "var products=[";
            var counter = 0;
            foreach (var item in products)
            {
                var type = string.Empty;
                if (item.ICProductType == ProductType.Service.ToString())
                {
                    type = "Dịch vụ";
                }
                else if (item.ICProductType == ProductType.ServicePackage.ToString())
                {
                    type = "Gói dịch vụ";
                }
                else if (item.ICProductType == ProductType.MedicalExamination.ToString())
                {
                    type = "Khám bệnh";
                }
                var image = !item.ICProductPicture.IsNullOrWhiteSpace() ? string.Format("{0}{1}", Constants.PATH_IMAGE_PRODUCT, item.ICProductPicture) : "/Content/images/noavatar.gif";
                jsContent += "{id:\"" + item.ICProductID + "\",name:\"" + item.ICProductName + "\", type:\"" + type + "\", price:\"" + Math.Round(item.ICProductPrice, 0) + "\", image:\"" + image + "\"}";
                if (counter++ < products.Count() - 1)
                {
                    jsContent += ",";
                }
            }
            jsContent += "];";

            return JavaScript(jsContent);
        }
        #endregion
    }
}