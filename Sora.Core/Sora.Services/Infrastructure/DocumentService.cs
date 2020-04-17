using Sora.Common.CommonObjects;
using Sora.Common.Extensions;
using Sora.EFCore.Infrastructure;
using Sora.Entites.IC;
using Sora.Services.Abstractions;
using Sora.Services.Infrastructure.Helpers;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sora.Services.Infrastructure
{
    public class DocumentService : IDocumentService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private IRepository<DocumentType> _documentTypeRepository => _unitOfWork.GetRepository<DocumentType>();
        private IRepository<Document> _documentRepository => _unitOfWork.GetRepository<Document>();
        #endregion

        #region Constructor
        public DocumentService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }
        #endregion

        #region Public methods
        public DocumentTypeViewModel GetDocumentType(int id)
        {
            try
            {
                var type = _documentTypeRepository.GetSingleById(id);
                return type.ToDocumentTypeViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void CreateDocumentType(DocumentTypeViewModel dto)
        {
            try
            {
                var type = dto.ToDocumentType();
                _documentTypeRepository.Add(type);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDocumentType(DocumentTypeViewModel dto)
        {
            try
            {
                var type = _documentTypeRepository.GetSingleById(dto.DocumentTypeID.GetValueOrDefault());
                type.CopyPropertiesFrom(dto);
                _documentTypeRepository.Update(type);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDocumentType(int id)
        {
            try
            {
                _documentTypeRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeViewModel> GetAllDocumentType()
        {
            try
            {
                var result = _documentTypeRepository.GetAll().ToList();
                return result.Select(x => x.ToDocumentTypeViewModel()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }


        public DocumentViewModel GetDocument(int id)
        {
            try
            {
                var document = _documentRepository.GetSingleById(id);
                return document.ToDocumentViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void CreateDocument(DocumentViewModel dto)
        {
            try
            {
                var document = dto.ToDocument();
                _documentRepository.Add(document);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDocument(DocumentViewModel dto)
        {
            try
            {
                var document = _documentRepository.GetSingleById(dto.DocumentID.GetValueOrDefault());
                document.CopyPropertiesFrom(dto);
                _documentRepository.Update(document);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDocument(int id)
        {
            try
            {
                _documentRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedResult<DocumentViewModel> FilterDocument(int page, int pageSize, int? documentTypeId, string search = "")
        {
            try
            {
                search = search.ToLower();
                var products = _documentRepository.GetAll()
                                                .Include(x => x.DocumentType)
                                                .Where(x=> !documentTypeId.HasValue || x.FK_DocumentTypeID == documentTypeId)
                                                .Where(x => (search == null || search.Trim() == string.Empty) || 
                                                    (x.Content.ToLower().Contains(search) || x.Signner.ToLower().Contains(search) || x.IssueBy.ToLower().Contains(search)))
                                                .OrderByDescending(x => x.DocumentDate)
                                                .Skip(page * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                var total = products.Count();
                var result = new PagedResult<DocumentViewModel>
                {
                    Items = products.Select(x => x.ToDocumentViewModel()).ToArray(),
                    PageIndex = page,
                    TotalCount = total,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(total / (double)pageSize)
                };

                return result;
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        #endregion
    }
}
