using Sora.Entites.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.IC
{
    public class DocumentType : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentTypeID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
