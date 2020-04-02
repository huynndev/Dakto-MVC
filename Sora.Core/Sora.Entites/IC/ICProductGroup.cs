using Sora.Entites.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.IC
{
    public class ICProductGroup : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ICProductGroupID { get; set; }

        public int? ICProductGroupParentID { get; set; }

        public string ICProductGroupType { get; set; }

        public string ICProductGroupName { get; set; }

        public string ICProductGroupDesc { get; set; }

        public virtual ICollection<ICProduct> Products { get; set; }

        [ForeignKey("ICProductGroupParentID")]
        public virtual ICProductGroup Parent { get; set; }
    }
}
