using Sora.Entites.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.ME
{
    public class MESpecialistType : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MESpecialistTypeID { get; set; }

        public string MESpecialistTypeName { get; set; }

        public string MESpecialistTypeDesc { get; set; }

        public string MESpecialistTypeCode { get; set; }

        public virtual ICollection<MESpecialist> MESpecialists { get; set; }
    }
}
