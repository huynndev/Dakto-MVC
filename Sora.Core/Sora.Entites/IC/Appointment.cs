using Sora.Entites.Interfaces;
using Sora.Entites.ME;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.IC
{
    public class Appointment : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime? FirstDay { get; set; }

        public string FirstTime { get; set; }

        public DateTime? SecondDay { get; set; }

        public string SecondTime { get; set; }

        public int? FK_MESpecialistID { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        [ForeignKey("FK_MESpecialistID")]
        public virtual MESpecialist MESpecialist { get; set; }
    }
}
