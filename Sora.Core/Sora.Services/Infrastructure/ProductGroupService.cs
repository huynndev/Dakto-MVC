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
    public class ProductGroupService : IProductGroupService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private IRepository<ICProductGroup> _productGroupRepository => _unitOfWork.GetRepository<ICProductGroup>();
        #endregion

        #region Constructor
        public ProductGroupService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }
        #endregion

        #region Public method
        public ProductGroupViewModel Get(int id)
        {
            try
            {
                var productGroup = _productGroupRepository.GetAll().FirstOrDefault(x => x.ICProductGroupID == id);
                if (productGroup == null)
                {
                    throw new Exception("Object not found!");
                }
                return productGroup.ToProductGroupViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Create(ProductGroupViewModel dto)
        {
            try
            {
                _productGroupRepository.Add(dto.ToICProductGroup());
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Update(ProductGroupViewModel dto)
        {
            try
            {
                var productGroup = _productGroupRepository.GetSingleById(dto.ICProductGroupID.GetValueOrDefault());
                productGroup.CopyPropertiesFrom(dto);
                _productGroupRepository.Update(productGroup);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _productGroupRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<ProductGroupViewModel> GetAll()
        {
            try
            {
                var result = _productGroupRepository.GetAll().Include(x => x.Parent).Include(x=>x.Products).ToList();
                return result.Select(x => x.ToProductGroupViewModel()).ToList();
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
