using Sora.Entites.IC;
using Sora.Hospital.Infrastructure.CommonObjects;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    [AuthorizeApp]
    //[CheckRole(Roles = "ContentManagement, ConfigManagement")]
    public class ArticlesController : BaseController
    {
        private string _urlApi = ConfigurationManager.AppSettings["Api"];
        private IArticleCategoryService _categoryService;
        private IArticleService _articleService;

        public ArticlesController(IArticleCategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        // GET: Admin/Articles
        public ActionResult ArticleListing(int? page, int? pageSize, string search, int? categoryId, bool isBlog = false)
        {
            ViewBag.Category = categoryId;
            if (isBlog)
            {
                ViewBag.ActiveMenu = "blogs-list";
                //category = blogParentCategoryId;
            }
            else
            {
                ViewBag.ActiveMenu = "news-list";
            }

            ArticleList articleListingModel = new ArticleList() { MainArticleList = new ArticleListingCO(), EnglishArticleList = new ArticleListingCO() };
            int totalRecords = 0;

            if (!page.HasValue) page = 1;
            if (!pageSize.HasValue) pageSize = 10;
            try
            {
                var list = _articleService.GetArticleByCategoryID(categoryId.GetValueOrDefault(), page.GetValueOrDefault(), pageSize.GetValueOrDefault(), "", out totalRecords);

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

            ViewBag.CurrentPage = page.GetValueOrDefault();
            ViewBag.PageSize = pageSize.GetValueOrDefault();
            ViewBag.TotalRecords = totalRecords;
            ViewBag.IsBlog = isBlog;
            return View(articleListingModel);
        }

        public ActionResult NewsCategory()
        {
            ViewBag.ActiveMenu = "news-cat";
            List<ArticleCategoryViewModel> list = new List<ArticleCategoryViewModel>();
            try
            {
                var categories = _categoryService.GetAllCategory();
                list = categories.ToList();
            }
            catch (Exception ex)
            {
                //log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
                return RedirectToAction("Error404", "PageNotFound");
            }

            if (TempData["Message"] != null) ViewBag.Message = TempData["Message"];
            if (TempData["SUCCESS"] != null) ViewBag.SUCCESS = TempData["SUCCESS"];
            TempData.Clear();
            return View(list);
        }

        [ValidateInput(false)]
        public ActionResult CreateNewsCategory()
        {
            ViewBag.ActiveMenu = "news-cat";
            var list = _categoryService.GetAllCategory();
            List<SelectListItem> catList = new List<SelectListItem>();
            catList = list.Where(o => o.ICArticleCategoryParentID == 0 || o.ICArticleCategoryParentID == null).Select(x => new SelectListItem() { Value = x.ICArticleCategoryID.ToString(), Text = x.ICArticleCategoryName }).ToList();
            ArticleCategoryViewModel category = new ArticleCategoryViewModel();
            category.ICArticleCategoryIsShowMenu = true;
            ViewBag.IsBlog = false;
            ViewBag.Categories = catList;
            return View("CategoryDetail", category);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveCategory(ArticleCategoryViewModel category)
        {
            ViewBag.ActiveMenu = "news-cat";
            try
            {
                if (category.ICArticleCategoryID > 0)
                {
                    _categoryService.Update(category);
                }
                else
                {
                    _categoryService.CreateArticleCategory(category);
                }
                TempData["SUCCESS"] = true;
                TempData["Message"] = "Lưu dữ liệu thành công!";
            }
            catch (Exception ex)
            {
                ViewBag.SUCCESS = false;
                ViewBag.Message = ex.Message;
            }
            ViewBag.IsBlog = false;
            return RedirectToAction("NewsCategory");
        }

        [HttpDelete]
        public ActionResult DeleteNewsCategory(int categoryId)
        {
            ArticleCategoryViewModel category = _categoryService.GetCategoryByID(categoryId);

            if (category == null)
            {
                TempData["SUCCESS"] = false;
                TempData["Message"] = "Không tìm thấy nội dung yêu cầu";
                return RedirectToAction("NewsCategory");
            }

            //if (category.MainCategory.IsDefault)
            //{
            //    TempData["SUCCESS"] = false;
            //    TempData["Message"] = string.Format(" {0} là danh mục mặc định, bạn không thể xóa mục này", category.MainCategory.CategoryName);
            //    return RedirectToAction("NewsCategory");
            //}
            try
            {
                _categoryService.Delete(category);
                TempData["SUCCESS"] = true;
                TempData["Message"] = string.Format(" {0} đã bị xóa khỏi hệ thống", category.ICArticleCategoryName);
                return RedirectToAction("NewsCategory");
            }
            catch (Exception ex)
            {
                //log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
                TempData["SUCCESS"] = false;
                TempData["Message"] = string.Format(ex.Message, category.ICArticleCategoryName);
                return RedirectToAction("NewsCategory");
            }
        }

        public ActionResult RecruitmentCategory()
        {
            return View();
        }

        public ActionResult CategoryDetail(int categoryId, bool isBlog = false)
        {
            ArticleCategoryViewModel category = new ArticleCategoryViewModel();
            ViewBag.ActiveMenu = "news-cat";
            List<SelectListItem> catList = new List<SelectListItem>();
            if (isBlog) ViewBag.ActiveMenu = "blogs-cat";
            try
            {
                category = _categoryService.GetCategoryByID(categoryId);
                var list = _categoryService.GetAllCategory();
                catList = list.Where(o => o.ICArticleCategoryID != categoryId && (o.ICArticleCategoryParentID == 0 || o.ICArticleCategoryParentID == null)).Select(x => new SelectListItem() { Value = x.ICArticleCategoryID.ToString(), Text = x.ICArticleCategoryName }).ToList();
            }
            catch (Exception ex)
            {
                TempData["SUCCESS"] = false;
                TempData["Message"] = ex.Message;
                //log.ErrorFormat("Error message: {0}\n{1}\n Access by {2}", ex.Message, ex.StackTrace, User.FullName);
                if (isBlog)
                {
                    return RedirectToAction("BlogsCategory");
                }
                return RedirectToAction("NewsCategory");
            }

            if (category == null)
            {
                return RedirectToAction("Error404", "PageNotFound");
            }
            ViewBag.IsBlog = isBlog;
            ViewBag.Categories = catList;
            return View(category);
        }

        private async Task<string> CallAPI(string url)
        {
            string data = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    data = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    //log.ErrorFormat("Error message: {0}. Access from {1}", ex.Message, url);
                }
            }
            return data;
        }

        public async Task<ActionResult> CreateArticle(string lang, bool isBlog = false)
        {
            ArticleViewModel article = new ArticleViewModel();
            List<SelectListItem> catList = new List<SelectListItem>();
            try
            {
                var list = _categoryService.GetAllCategory();
                ViewBag.ActiveMenu = "news-list";
                catList = list.Select(x => new SelectListItem() { Value = x.ICArticleCategoryID.ToString(), Text = x.ICArticleCategoryName }).ToList();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
            }
            article.ICArticleIsShowHome = true;
            ViewBag.Categories = catList;
            ViewBag.IsBlog = isBlog;
            ViewBag.IsCreate = true;
            ViewBag.websiteURL = "#";
            return View("ArticleDetail", article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ArticleDetail(ArticleViewModel article, HttpPostedFileBase image, bool isBlog = false)
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
                if(article.ICArticleID > 0)
                {
                    _articleService.Update(article);
                }
                else
                {
                    _articleService.CreateArticle(article);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
            }
            ViewBag.IsBlog = isBlog;
            return RedirectToAction("ArticleListing");
        }

        [HttpGet]
        public async Task<ActionResult> ArticleDetail(int articleId, bool isBlog = false)
        {
            string websiteURL = "#";
            ViewBag.websiteURL = websiteURL;

            List<SelectListItem> catList = new List<SelectListItem>();
            ArticleViewModel article = new ArticleViewModel();
            try
            {
                article = _articleService.GetArticleByID(articleId);
                var list = _categoryService.GetAllCategory();
                ViewBag.ActiveMenu = "news-list";
                catList = list.Select(x => new SelectListItem() { Value = x.ICArticleCategoryID.ToString(), Text = x.ICArticleCategoryName }).ToList();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
            }
            ViewBag.Categories = catList;
            ViewBag.IsBlog = isBlog;
            ViewBag.IsCreate = false;
            return View(article);
        }

        [HttpDelete]
        public ActionResult DeleteArticle(int articleId, int categoryId, bool isBlog = false)
        {
            ArticleViewModel article = _articleService.GetArticleByID(articleId);
            ArticleCategoryViewModel articleCategory = _categoryService.GetCategoryByID(categoryId);
            if (article == null || articleCategory == null)
            {
                TempData["SUCCESS"] = false;
                TempData["Message"] = "Không tìm thấy nội dung yêu cầu";
                return Redirect(Request.UrlReferrer.ToString());
            }
                
            try
            {
                _articleService.Delete(article);
                TempData["SUCCESS"] = true;
                TempData["Message"] = string.Format(" {0} đã bị xóa khỏi hệ thống", article.ICArticleTitle);
                //return RedirectToAction("ArticleListing", new { categoryId = categoryId, isBlog = isBlog });
                return RedirectToAction("ArticleListing");
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error message: {0}. Access by {1}", ex.Message, User.FullName);
                TempData["SUCCESS"] = false;
                TempData["Message"] = string.Format(ex.Message, article.ICArticleTitle);
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}