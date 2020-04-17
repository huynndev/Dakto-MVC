using Sora.Entites.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sora.Entites.IC
{
    public class Document : Auditable
    {
        public int DocumentID { get; set; }

        public string DocumentNo { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string Signner { get; set; }

        public string Content { get; set; }

        public string IssueBy { get; set; }

        public string File { get; set; }

        public int? FK_DocumentTypeID { get; set; }

        [ForeignKey("FK_DocumentTypeID")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
