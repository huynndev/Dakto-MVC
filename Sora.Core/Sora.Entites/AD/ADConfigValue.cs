using Sora.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Entites.AD
{
    [Table("ADConfigValues")]
    public class ADConfigValue : Auditable
    {
        public int ADConfigValueID { get; set; }

    }
}
