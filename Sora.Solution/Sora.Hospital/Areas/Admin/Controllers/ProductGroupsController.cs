﻿using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    public class ProductGroupsController : BaseController
    {
        #region Properties
        private readonly IProductGroupService _productGroupService;
        #endregion

        #region Constructor
        public ProductGroupsController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }
        #endregion

        #region Public method
        // GET: Admin/ProductGroups
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "product-group-index";
            var result = _productGroupService.GetAll();
            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];
            return View(result);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveMenu = "product-group-index";
            var result = _productGroupService.Get(id);
            ViewData["Groups"] = _productGroupService.GetAll();
            return View(result);
        }

        public ActionResult Create()
        {
            ViewBag.ActiveMenu = "product-group-index";
            ViewData["Groups"] = _productGroupService.GetAll();
            ProductGroupViewModel productGroup = new ProductGroupViewModel();
            return View("Detail", productGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProductGroupViewModel dto)
        {
            ViewBag.ActiveMenu = "product-group-index";
            var isCreate = true;
            try
            {
                if (!dto.ICProductGroupID.HasValue)
                {
                    _productGroupService.Create(dto);
                    TempData["Message"] = "Tạo mới nhóm sản phẩm thành công.";
                }
                else
                {
                    isCreate = false;
                    _productGroupService.Update(dto);
                    TempData["Message"] = "Cập nhật nhóm sản phẩm thành công.";
                }
                TempData["Success"] = true;
                return Redirect("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Message = isCreate ? "Tạo mới nhóm sản phẩm thất bại, vui lòng thử lại." : "Cập nhật nhóm sản phẩm thất bại, vui lòng thử lại.";
                ViewData["Groups"] = _productGroupService.GetAll();
                return View("Detail", dto);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _productGroupService.Delete(id);
                TempData["Success"] = true;
                TempData["Message"] = "Xóa nhóm sản phẩm thành công.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = "Xóa nhóm sản phẩm thất bại. Vui lòng thử lại";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}