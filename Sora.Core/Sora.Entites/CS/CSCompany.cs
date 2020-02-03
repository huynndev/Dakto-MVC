using Sora.Entites.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.CS
{
    [Table("CSCompany")]
    public class CSCompany : Auditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CSCompanyID { get; set; }

        [Required]
        [StringLength(256)]
        public string CSCompanyName { get; set; }

        [StringLength(512)]
        public string CSCompanyDesc { get; set; }

        [StringLength(100)]
        public string CSCompanyEmail { get; set; }

        [StringLength(100)]
        public string CSCompanyPhone { get; set; }

        [StringLength(512)]
        public string CSCompanyAddress { get; set; }

        [StringLength(256)]
        public string CSCompanyAvatar { get; set; }

        [StringLength(50)]
        public string CSCompanyLang { get; set; }

        [StringLength(256)]
        public string CSCompanyWebsite { get; set; }
    }
}