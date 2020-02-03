using Sora.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Entites.AD
{
    [Table("ADUsers")]
    public class ADUser : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ADUserID { get; set; }

        public string ADUserName { get; set; }

        public string ADPassword { get; set; }

        public bool? ADUserIsActive { get; set; }
    }
}
