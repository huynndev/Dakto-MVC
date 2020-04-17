using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.ViewModels
{
    public class DocumentViewModel
    {
        public int? DocumentID { get; set; }

        public string DocumentNo { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string Signner { get; set; }

        public string Content { get; set; }

        public string IssueBy { get; set; }

        public string File { get; set; }

        public int? FK_DocumentTypeID { get; set; }

        public DocumentTypeViewModel Type { get; set; }
    }
}
