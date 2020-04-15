using Sora.Common.Enums;
using Sora.Hospital.Infrastructure.CommonObjects;
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
    public class GuideController : BaseController
    {
        private IArticleService _articleService;

        public GuideController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public ActionResult Index(int? page, int? pageSize, string search)
        {

            ViewBag.ActiveMenu = "index-guide";

            ArticleList articleListingModel = new ArticleList() { MainArticleList = new ArticleListingCO(), EnglishArticleList = new ArticleListingCO() };
            int totalRecords = 0;

            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;
            try
            {
                var list = _articleService.GetArticleByCategoryID(0, page.GetValueOrDefault() - 1, pageSize.GetValueOrDefault(), "", out totalRecords, ArticleType.Guide, search);

                articleListingModel.MainArticleList.List = list.ToList();
                articleListingModel.MainArticleList.Lang = "vn";
                articleListingModel.MainArticleList.TotalRecords = totalRecords;
                articleListingModel.MainArticleList.PageSize = pageSize.GetValueOrDefault();
                articleListingModel.MainArticleList.CurrentPage = page.GetValueOrDefault() - 1;
                articleListingModel.MainArticleList.TotalPages = totalRecords % pageSize.GetValueOrDefault() == 0 ? totalRecords / pageSize.GetValueOrDefault() : (totalRecords / pageSize.GetValueOrDefault()) + 1;
            }
            catch (Exception ex)
            {
                //log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
                RedirectToAction("Error404", "PageNotFound");
            }


            if (TempData["Success"] != null)
                ViewBag.Success = TempData["Success"];
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"];

            ViewBag.CurrentPage = page.GetValueOrDefault() - 1;
            ViewBag.PageSize = pageSize.GetValueOrDefault();
            ViewBag.TotalRecords = totalRecords;
            return View(articleListingModel);
        }

        public ActionResult Create()
        {
            ArticleViewModel article = new ArticleViewModel();
            article.ICArticleIsShowHome = true;
            article.ICArticleType = ArticleType.Guide.ToString();
            ViewBag.IsCreate = true;
            ViewBag.websiteURL = "#";
            return View("Detail", article);
        }

        [HttpGet]
        public ActionResult Detail(int articleId)
        {
            ViewBag.ActiveMenu = "index-guide";
            string websiteURL = "#";
            ViewBag.websiteURL = websiteURL;

            ArticleViewModel article = new ArticleViewModel();
            try
            {
                article = _articleService.GetArticleByID(articleId);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
            }
            ViewBag.IsCreate = false;
            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ArticleViewModel article, HttpPostedFileBase image)
        {
            string websiteURL = "#";
            ViewBag.websiteURL = websiteURL;
            if (image != null && image.ContentLength > 0)
            {
                // Kiểm tra định dạng file
                List<string> acceptFile = new List<string>() { ".png", ".jpg", ".gif", ".jpeg", ".pdf" };
                string savePath = Server.MapPath("~/Content/Upload/Article/");
                string fileExtension = Path.GetExtension(image.FileName);
                string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                if (acceptFile.Exists(x => x.EndsWith(fileExtension.ToLower())))
                {
                    var dateString = DateTime.Now.ToString("_ddMMyy_hhmmss");
                    var filePath = Path.Combine(savePath, fileName + dateString + fileExtension);
                    image.SaveAs(filePath);
                    article.ICArticlePicture = string.Format("{0}{1}{2}", fileName, dateString, fileExtension);
                }
            }

            try
            {
                if (article.ICArticleID > 0)
                {
                    _articleService.Update(article);
                    TempData["Message"] = "Cập nhật bài viết hướng dẫn bệnh nhân thành công.";
                }
                else
                {
                    _articleService.CreateArticle(article);
                    TempData["Message"] = "Cập nhật bài viết hướng dẫn bệnh nhân thành công.";
                }
                TempData["Success"] = true;
            }
            catch (Exception ex)
            {
                TempData["Success"] = false;
                TempData["Message"] = article.ICArticleID == 0 ? "Tạo mới bài viết hướng dẫn bệnh nhân thất bại, vui lòng thử lại." : "Cập nhật bài viết hướng dẫn bệnh nhân thất bại, vui lòng thử lại.";
            }
            return RedirectToAction("Index");
        }


        [HttpDelete]
        public ActionResult Delete(int articleId)
        {
            ArticleViewModel article = _articleService.GetArticleByID(articleId);
            if (article == null || !article.ICArticleType.Equals(ArticleType.Guide.ToString()))
            {
                TempData["SUCCESS"] = false;
                TempData["Message"] = "Không tìm thấy nội dung yêu cầu";
            }
            else
            {
                try
                {
                    _articleService.Delete(article);
                    TempData["SUCCESS"] = true;
                    TempData["Message"] = string.Format(" {0} đã bị xóa khỏi hệ thống", article.ICArticleTitle);
                    //return RedirectToAction("ArticleListing", new { categoryId = categoryId, isBlog = isBlog });
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
                    TempData["SUCCESS"] = false;
                    TempData["Message"] = string.Format(ex.Message, article.ICArticleTitle);
                }
            }
            return RedirectToAction("Index");
        }
    }
}