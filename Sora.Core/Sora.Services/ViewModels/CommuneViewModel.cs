using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class CommuneViewModel
    {
        public int? CommuneID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ShortContent { get; set; }

        [AllowHtml]
        public string FullContent { get; set; }

        public string Image { get; set; }
    }
}
