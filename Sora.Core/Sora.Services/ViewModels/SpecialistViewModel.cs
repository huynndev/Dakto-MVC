using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class SpecialistViewModel
    {
        public int? MESpecialistID { get; set; }

        public int? FK_MEDoctorID { get; set; }

        public int? FK_MESpecialistTypeID { get; set; }

        public string MESpecialistName { get; set; }

        public string MESpecialistCode { get; set; }

        [AllowHtml]
        public string MESpecialistDesc { get; set; }

        public int TotalDoctor { get; set; }

        public DoctorViewModel ChiefDoctor { get; set; }

        public SpecialistTypeViewModel Type { get; set; }

        public DoctorViewModel[] Doctors { get; set; }
    }
}
