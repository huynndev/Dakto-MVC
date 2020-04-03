using Sora.Entites.Interfaces;
using Sora.Entites.ME;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sora.Entites.IC
{
    public class ICProduct : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ICProductID { get; set; }

        public int? FK_ICProductGroupID { get; set; }

        public int? FK_MESpecialistID { get; set; }

        public string ICProductType { get; set; }

        public string ICProductName { get; set; }

        public string ICProductTitle { get; set; }

        public string ICProductContent { get; set; }

        public bool ICProductIsShowWeb { get; set; }

        public decimal ICProductPrice { get; set; }

        public string ICProductPicture { get; set; }

        public bool ICProductIsDetail { get; set; }

        [ForeignKey("FK_MESpecialistID")]
        public virtual MESpecialist MESpecialist { get; set; }

        [ForeignKey("FK_ICProductGroupID")]
        public virtual ICProductGroup ICProductGroup { get; set; }

        public virtual ICollection<ICProductDetail> ICProductDetails { get; set; }
    }
}
