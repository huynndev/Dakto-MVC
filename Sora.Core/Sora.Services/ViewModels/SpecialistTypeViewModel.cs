using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class SpecialistTypeViewModel
    {
        public int? MESpecialistTypeID { get; set; }

        public string MESpecialistTypeName { get; set; }

        [AllowHtml]
        public string MESpecialistTypeDesc { get; set; }

        public string MESpecialistTypeCode { get; set; }
    }
}
