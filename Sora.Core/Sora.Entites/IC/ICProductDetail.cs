using Sora.Entites.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.IC
{
    public class ICProductDetail : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ICProductDetailID { get; set; }

        public int? FK_ICProductID { get; set; }

        public decimal ICProductDetailQty { get; set; }

        public decimal ICProductDetailPrice { get; set; }

        public decimal ICProductDetailTotalAmout { get; set; }

        [ForeignKey("FK_ICProductID")]
        public virtual ICProduct ICProduct { get; set; }
    }
}
