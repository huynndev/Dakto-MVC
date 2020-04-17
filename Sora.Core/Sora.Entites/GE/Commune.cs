using Sora.Entites.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.GE
{
    public class Commune : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommuneID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ShortContent { get; set; }

        public string FullContent { get; set; }

        public string Image { get; set; }
    }
}
