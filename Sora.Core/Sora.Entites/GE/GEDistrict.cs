using Sora.Entites.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.GE
{
    public class GEDistrict : Auditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GEDistrictID { get; set; }

        public int? FK_GEStateProvinceID { get; set; }

        [StringLength(255)]
        public string GEDistrictName { get; set; }

        [StringLength(50)]
        public string GEDistrictType { get; set; }

        public int? GEDistrictOrder { get; set; }

        public virtual GEStateProvince GEStateProvince { get; set; }
    }
}