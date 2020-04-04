using Sora.Entites.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.IC
{
    public class ICArticle : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ICArticleID { get; set; }

        public int? FK_ICArticleCategoryID { get; set; }

        [StringLength(256)]
        public string ICArticleFriendlyUrl { get; set; }

        [StringLength(256)]
        public string ICArticleTitle { get; set; }

        public DateTime? ICArticleDate { get; set; }

        [StringLength(1024)]
        public string ICArticleShortContent { get; set; }

        public string ICArticleContent { get; set; }

        [StringLength(256)]
        public string ICArticlePicture { get; set; }

        [StringLength(100)]
        public string ICArticleMetaKeyword { get; set; }

        [StringLength(512)]
        public string ICArticleMetaDesc { get; set; }

        [StringLength(256)]
        public string ICArticleTags { get; set; }

        public bool? ICArticleIsShowHome { get; set; }

        public bool ICArticleIsFeatured { get; set; }

        public int ICArticleFeaturedSortOrder { get; set; }

        [ForeignKey("FK_ICArticleCategoryID")]
        public virtual ICArticleCategory ICArticleCategory { get; set; }
    }
}