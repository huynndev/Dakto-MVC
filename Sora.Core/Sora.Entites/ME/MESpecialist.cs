using Sora.Entites.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.ME
{
    public class MESpecialist : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MESpecialistID { get; set; }

        public int? FK_MEDoctorID { get; set; }

        public string MESpecialistType { get; set; }

        public string MESpecialistName { get; set; }

        public string MESpecialistDesc { get; set; }

        public string MESpecialistCode { get; set; }

        public virtual ICollection<MEDoctorSpecialist> MEDoctorSpecialists { get; set; }

        [ForeignKey("FK_MEDoctorID")]
        public virtual MEDoctor MEDoctor { get; set; }
    }
}
