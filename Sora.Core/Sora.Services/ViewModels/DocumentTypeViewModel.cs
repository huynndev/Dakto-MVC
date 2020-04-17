using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class DocumentTypeViewModel
    {
        public int? DocumentTypeID { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }
    }
}
