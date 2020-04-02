using Sora.Common.Extensions;
using Sora.Entites.IC;
using Sora.Entites.ME;
using Sora.Services.ViewModels;
using System.Linq;

namespace Sora.Services.Infrastructure.Helpers
{
    public static class ConvertEntityToViewModelHelper
    {
        public static SpecialistViewModel ToSpecialistViewModel(this MESpecialist entity)
        {
            if(entity == null)
            {
                return null;
            }
            SpecialistViewModel dto = new SpecialistViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MESpecialistID = entity.MESpecialistID;
            dto.TotalDoctor = entity.MEDoctorSpecialists == null ? 0 : entity.MEDoctorSpecialists.Count(x => x.FK_MEDoctorID.HasValue);
            dto.ChiefDoctor = entity.MEDoctor.ToShortDoctorDto();
            return dto;
        }

        public static SpecialistViewModel ToSpecialistViewModel(this MEDoctorSpecialist entity)
        {
            if(entity.MESpecialist == null)
            {
                return null;
            }
            SpecialistViewModel dto = new SpecialistViewModel();
            dto.CopyPropertiesFrom(entity.MESpecialist);
            dto.MESpecialistID = entity.MESpecialist.MESpecialistID;
            return dto;
        }

        public static ShortSpecialistDto ToShortSpecialistDto(this MESpecialist entity)
        {
            if(entity == null)
            {
                return null;
            }
            ShortSpecialistDto dto = new ShortSpecialistDto();
            dto.CopyPropertiesFrom(entity);
            return dto;
        }

        public static DoctorViewModel ToDoctorViewModel(this MEDoctor entity)
        {
            if (entity == null)
                return null;

            DoctorViewModel dto = new DoctorViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MEDoctorID = entity.MEDoctorID;
            return dto;
        }

        public static DoctorViewModel ToDoctorViewModel(this MEDoctorSpecialist entity, SpecialistViewModel[] specialists)
        {
            if (entity.MEDoctor == null)
                return null;

            DoctorViewModel dto = new DoctorViewModel();
            dto.CopyPropertiesFrom(entity.MEDoctor);
            dto.MEDoctorID = entity.MEDoctor.MEDoctorID;
            dto.Specialists = specialists;
            return dto;
        }
        public static DoctorViewModel ToDoctorViewModel(this MEDoctor entity, SpecialistViewModel[] specialists)
        {
            if (entity == null)
                return null;

            DoctorViewModel dto = new DoctorViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MEDoctorID = entity.MEDoctorID;
            dto.Specialists = specialists;
            dto.MESpecialistIds = specialists.Select(x => x.MESpecialistID.ToString()).ToArray();
            return dto;
        }

        public static ShortDoctorDto ToShortDoctorDto(this MEDoctor entity)
        {
            if (entity == null)
                return null;
            ShortDoctorDto dto = new ShortDoctorDto();
            dto.CopyPropertiesFrom(entity);
            return dto;
        }

        public static ProductGroupViewModel ToProductGroupViewModel(this ICProductGroup entity)
        {
            if (entity == null)
                return null;
            ProductGroupViewModel dto = new ProductGroupViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.ICProductGroupID = entity.ICProductGroupID;
            dto.TotalProduct = entity.Products.IsNullOrEmpty() ? 0 : entity.Products.Count();
            dto.Parent = entity.Parent.ToShortProductGroupDto();
            return dto;
        }

        public static ShortProductGroupDto ToShortProductGroupDto(this ICProductGroup entity)
        {
            if (entity == null)
                return null;
            ShortProductGroupDto dto = new ShortProductGroupDto();
            dto.CopyPropertiesFrom(entity);
            return dto;
        }

        public static ProductViewModel ToProductViewModel(this ICProduct entity)
        {
            if (entity == null)
                return null;
            ProductViewModel dto = new ProductViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.ICProductID = entity.ICProductID;
            dto.ProductGroup = entity.ICProductGroup.ToProductGroupViewModel();
            dto.Specialist = entity.MESpecialist.ToShortSpecialistDto();
            return dto;
        }

        public static ShortProductDto ToShortProductDto(this ICProduct entity)
        {
            if (entity == null)
                return null;
            ShortProductDto dto = new ShortProductDto();
            dto.CopyPropertiesFrom(entity);
            return dto;
        }
    }
}
