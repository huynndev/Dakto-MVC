using Sora.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Entites.IC
{
    public class ICArticleCategory : Auditable
    {
        public ICArticleCategory()
        {
            ICArticles = new HashSet<ICArticle>();
        }
   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ICArticleCategoryID { get; set; }

        [StringLength(50)]
        public string ICArticleCategoryNo { get; set; }

        [StringLength(256)]
        public string ICArticleCategoryName { get; set; }

        [StringLength(512)]
        public string ICArticleCategoryDesc { get; set; }

        public int? ICArticleCategorySortOrder { get; set; }

        public bool? ICArticleCategoryIsShowMenu { get; set; }

        public int? ICArticleCategoryParentID { get; set; }

        public virtual ICollection<ICArticle> ICArticles { get; set; }
    }
}
