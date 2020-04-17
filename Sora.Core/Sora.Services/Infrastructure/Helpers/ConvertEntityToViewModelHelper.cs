using Newtonsoft.Json;
using Sora.Common.Constants;
using Sora.Common.Extensions;
using Sora.Entites.GE;
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
            if (entity == null)
            {
                return null;
            }
            SpecialistViewModel dto = new SpecialistViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MESpecialistID = entity.MESpecialistID;
            dto.TotalDoctor = entity.MEDoctorSpecialists.IsNullOrEmpty() ? 0 : entity.MEDoctorSpecialists.Count(x => x.FK_MEDoctorID.HasValue);
            dto.ChiefDoctor = entity.MEDoctor.ToDoctorViewModel();
            dto.Type = entity.MESpecialistType.ToSpecialistTypeViewModel();
            dto.Doctors = entity.MEDoctorSpecialists.IsNullOrEmpty() ? null : entity.MEDoctorSpecialists.Select(x => x.MEDoctor.ToDoctorViewModel().FullUrlImageDoctor()).ToArray();
            return dto;
        }

        public static SpecialistTypeViewModel ToSpecialistTypeViewModel(this MESpecialistType entity)
        {
            if (entity == null)
            {
                return null;
            }
            SpecialistTypeViewModel dto = new SpecialistTypeViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MESpecialistTypeID = entity.MESpecialistTypeID;
            return dto;
        }

        public static SpecialistTypeDetailDto ToSpecialistTypeDetailDto(this MESpecialistType entity)
        {
            if (entity == null)
            {
                return null;
            }
            SpecialistTypeDetailDto dto = new SpecialistTypeDetailDto();
            dto.CopyPropertiesFrom(entity);
            dto.Specialists = entity.MESpecialists.IsNullOrEmpty() ? null : entity.MESpecialists.Select(x => x.ToSpecialistViewModel()).ToArray();
            return dto;
        }

        public static SpecialistViewModel ToSpecialistViewModel(this MEDoctorSpecialist entity)
        {
            if (entity.MESpecialist == null)
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
            if (entity == null)
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

        public static ProductDetailDto ToProductDetailDto(this ICProductDetail entity)
        {
            if (entity == null)
                return null;
            ProductDetailDto dto = new ProductDetailDto();
            dto.CopyPropertiesFrom(entity);
            dto.Product = entity.ICProduct.ToProductViewModel();
            return dto;
        }

        public static ProductDetailDto ToProductDetailDto(this ICProductDetail entity, ProductViewModel product)
        {
            if (entity == null)
                return null;
            ProductDetailDto dto = new ProductDetailDto();
            dto.CopyPropertiesFrom(entity);
            dto.Product = product;
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

        public static DoctorViewModel FullUrlImageDoctor(this DoctorViewModel dto)
        {
            if (dto == null)
                return null;
            dto.MEDoctorPicture = dto.MEDoctorPicture.IsNullOrWhiteSpace()
                ? Constants.PATH_IMAGE.GenerateFullUrl("noavatar.gif")
                : Constants.PATH_IMAGE_DOCTOR.GenerateFullUrl(dto.MEDoctorPicture);
            return dto;
        }

        public static ProductViewModel FullUrlImageProduct(this ProductViewModel dto)
        {
            if (dto == null)
                return null;
            dto.ICProductPicture = dto.ICProductPicture.IsNullOrWhiteSpace()
                ? Constants.PATH_IMAGE.GenerateFullUrl("noavatar.gif")
                : Constants.PATH_IMAGE_PRODUCT.GenerateFullUrl(dto.ICProductPicture);
            return dto;
        }

        public static CompanyViewModel FullUrlImageCompany(this CompanyViewModel dto)
        {
            if (dto == null)
                return null;
            dto.CSCompanyAvatar = dto.CSCompanyAvatar.IsNullOrWhiteSpace()
                ? Constants.PATH_IMAGE.GenerateFullUrl("noavatar.gif")
                : Constants.PATH_IMAGE_COMPANY.GenerateFullUrl(dto.CSCompanyAvatar);
            return dto;
        }

        public static CommuneViewModel ToCommuneViewModel(this Commune entity)
        {
            if (entity == null)
                return null;
            CommuneViewModel dto = new CommuneViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.CommuneID = entity.CommuneID;
            return dto;
        }

        public static DocumentTypeViewModel ToDocumentTypeViewModel(this DocumentType entity)
        {
            if (entity == null)
                return null;
            DocumentTypeViewModel dto = new DocumentTypeViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.DocumentTypeID = entity.DocumentTypeID;
            return dto;
        }

        public static DocumentViewModel ToDocumentViewModel(this Document entity)
        {
            if (entity == null)
                return null;
            DocumentViewModel dto = new DocumentViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.DocumentID = entity.DocumentID;
            dto.Type = entity.DocumentType.ToDocumentTypeViewModel();
            return dto;
        }
    }
}
