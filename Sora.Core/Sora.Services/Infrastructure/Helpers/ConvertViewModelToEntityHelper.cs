using Sora.Common.Extensions;
using Sora.Entites.GE;
using Sora.Entites.IC;
using Sora.Entites.ME;
using Sora.Services.ViewModels;

namespace Sora.Services.Infrastructure.Helpers
{
    public static class ConvertViewModelToEntityHelper
    {
        public static MESpecialist ToMESpecialist(this SpecialistViewModel dto)
        {
            MESpecialist entity = new MESpecialist();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static MEDoctor ToMEDoctor(this DoctorViewModel dto)
        {
            MEDoctor entity = new MEDoctor();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static MESpecialistType ToMESpecialistType(this SpecialistTypeViewModel dto)
        {
            MESpecialistType entity = new MESpecialistType();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static ICProductGroup ToICProductGroup(this ProductGroupViewModel dto)
        {
            ICProductGroup entity = new ICProductGroup();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static ICProduct ToICProduct(this ProductViewModel dto)
        {
            ICProduct entity = new ICProduct();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static Commune ToCommune(this CommuneViewModel dto)
        {
            Commune entity = new Commune();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static DocumentType ToDocumentType(this DocumentTypeViewModel dto)
        {
            DocumentType entity = new DocumentType();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static Document ToDocument(this DocumentViewModel dto)
        {
            Document entity = new Document();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }
    }
}
