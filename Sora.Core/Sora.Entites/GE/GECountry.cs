using Sora.Entites.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.GE
{
    public class GECountry : Auditable
    {
        public GECountry()
        {
            GEStateProvinces = new HashSet<GEStateProvince>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GECountryID { get; set; }

        [StringLength(10)]
        public string GECountryNo { get; set; }

        [StringLength(256)]
        public string GECommonName { get; set; }

        [StringLength(256)]
        public string GEFormalName { get; set; }

        [StringLength(50)]
        public string GECountryType { get; set; }

        public int? GECountryOrder { get; set; }

        public virtual ICollection<GEStateProvince> GEStateProvinces { get; set; }
    }
}