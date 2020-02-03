using Sora.Entites.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.GE
{
    public class GEStateProvince : Auditable
    {
        public GEStateProvince()
        {
            GEDistricts = new HashSet<GEDistrict>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GEStateProvinceID { get; set; }

        public int? FK_GECountryID { get; set; }

        [StringLength(255)]
        public string GEStateProvinceName { get; set; }

        [StringLength(50)]
        public string GEStateProvinceType { get; set; }

        public int? GEStateProvinceOrder { get; set; }

        public virtual GECountry GECountry { get; set; }

        public virtual ICollection<GEDistrict> GEDistricts { get; set; }
    }
}