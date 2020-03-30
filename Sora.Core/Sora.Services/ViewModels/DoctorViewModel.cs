using System.Web.Mvc;

namespace Sora.Services.ViewModels
{
    public class DoctorViewModel
    {
        public int? MEDoctorID { get; set; }

        public string MEDoctorType { get; set; }

        public string MEDoctorSalutation { get; set; }

        public string MEDoctorName { get; set; }

        [AllowHtml]
        public string MEDoctorDesc { get; set; }

        [AllowHtml]
        public string MEDoctorCertificate { get; set; }

        [AllowHtml]
        public string MEDoctorExperiencee { get; set; }

        [AllowHtml]
        public string MEDoctorIntensive { get; set; }

        public string MEDoctorPicture { get; set; }

        public string[] MESpecialistIds { get; set; }

        public SpecialistViewModel[] Specialists { get; set; }
    }
}
