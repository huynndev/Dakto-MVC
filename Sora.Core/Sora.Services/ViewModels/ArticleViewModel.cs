using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.ViewModels
{
    public class ArticleViewModel
    {
        public int ICArticleID { get; set; }

        public int? FK_ICArticleCategoryID { get; set; }

        public string ICArticleFriendlyUrl { get; set; }

        public string ICArticleTitle { get; set; }

        public DateTime? ICArticleDate { get; set; }

        public string ICArticleShortContent { get; set; }

        public string ICArticleContent { get; set; }

        public string ICArticlePicture { get; set; }

        public string ICArticleMetaKeyword { get; set; }

        public string ICArticleMetaDesc { get; set; }

        public string ICArticleTags { get; set; }

        public bool ICArticleIsShowHome { get; set; }

        public bool ICArticleIsFeatured { get; set; } = false;

        public int ICArticleFeaturedSortOrder { get; set; }
    }
}
