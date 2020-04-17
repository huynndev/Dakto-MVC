using Sora.Entites.Interfaces;

namespace Sora.Entites.IC
{
    public class DocumentType : Auditable
    {
        public int DocumentTypeID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
