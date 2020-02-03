using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
