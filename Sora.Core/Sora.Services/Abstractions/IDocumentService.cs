using Sora.Common.CommonObjects;
using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface IDocumentService
    {
        DocumentTypeViewModel GetDocumentType(int id);

        void CreateDocumentType(DocumentTypeViewModel dto);

        void UpdateDocumentType(DocumentTypeViewModel dto);

        void DeleteDocumentType(int id);

        List<DocumentTypeViewModel> GetAllDocumentType();

        DocumentViewModel GetDocument(int id);

        void CreateDocument(DocumentViewModel dto);

        void UpdateDocument(DocumentViewModel dto);

        void DeleteDocument(int id);

        PagedResult<DocumentViewModel> FilterDocument(int page, int pageSize, int? documentTypeId, string search = "");
    }
}
