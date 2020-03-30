using Sora.Entites.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.ME
{
    public class MEDoctorSpecialist : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MEDoctorSpecialistID { get; set; }

        public int? FK_MEDoctorID { get; set; }

        public int? FK_MESpecialistID { get; set; }

        [ForeignKey("FK_MESpecialistID")]
        public virtual MESpecialist MESpecialist { get; set; }

        [ForeignKey("FK_MEDoctorID")]
        public virtual MEDoctor MEDoctor { get; set; }
    }
}
