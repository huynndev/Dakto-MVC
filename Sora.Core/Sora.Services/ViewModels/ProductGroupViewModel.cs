using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class ProductGroupViewModel
    {
        public int? ICProductGroupID { get; set; }

        public int? ICProductGroupParentID { get; set; }

        public string ICProductGroupType { get; set; }

        public string ICProductGroupName { get; set; }

        [AllowHtml]
        public string ICProductGroupDesc { get; set; }

        public int TotalProduct { get; set; }

        public ShortProductGroupDto Parent { get; set; }
    }
}
