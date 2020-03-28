using System;
using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class ArticleCategoryViewModel
    {
        public int ICArticleCategoryID { get; set; }

        [AllowHtml]
        public string ICArticleCategoryName { get; set; }

        public string ICArticleCategoryNo { get; set; }

        [AllowHtml]
        public string ICArticleCategoryDesc { get; set; }

        public int? ICArticleCategorySortOrder { get; set; }

        public bool ICArticleCategoryIsShowMenu { get; set; }

        public int? ICArticleCategoryParentID { get; set; }

        public int CountICArticle { get; set; }

        public DateTime? AACreatedDate { get; set; }

        public DateTime? AAUpdatedDate { get; set; }
    }
}
