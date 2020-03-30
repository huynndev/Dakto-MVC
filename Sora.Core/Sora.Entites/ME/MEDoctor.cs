using Sora.Entites.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.ME
{
    public class MEDoctor : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MEDoctorID { get; set; }

        public string MEDoctorType { get; set; }

        public string MEDoctorSalutation { get; set; }

        public string MEDoctorName { get; set; }

        public string MEDoctorDesc { get; set; }

        public string MEDoctorCertificate { get; set; }

        public string MEDoctorExperiencee { get; set; }

        public string MEDoctorIntensive { get; set; }

        public string MEDoctorPicture { get; set; }

        public virtual ICollection<MEDoctorSpecialist> MEDoctorSpecialists { get; set; }
    }
}
