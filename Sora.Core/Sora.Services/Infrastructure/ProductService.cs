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
    public class ProductService : IProductService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private IRepository<ICProduct> _productRepository => _unitOfWork.GetRepository<ICProduct>();
        private IRepository<ICProductDetail> _productDetailRepository => _unitOfWork.GetRepository<ICProductDetail>();
        #endregion

        #region Constructor
        public ProductService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }
        #endregion

        #region Public method
        public void Create(ProductViewModel dto)
        {
            try
            {
                _productRepository.Add(dto.ToICProduct());
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
                _productRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public ProductViewModel Get(int id)
        {
            try
            {
                var product = _productRepository.GetAll().Include(x => x.ICProductGroup).Include(x => x.MESpecialist).FirstOrDefault(x => x.ICProductID == id);
                if (product == null)
                {
                    throw new Exception("Object not found!");
                }
                return product.ToProductViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Update(ProductViewModel dto)
        {
            try
            {
                var product = _productRepository.GetSingleById(dto.ICProductID.GetValueOrDefault());
                product.CopyPropertiesFrom(dto);
                _productRepository.Update(product);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }
        public PagedResult<ProductViewModel> Filter(int page, int pageSize, int? groupId, bool? isShowWeb, string search = null)
        {
            try
            {
                var products = _productRepository.GetAll()
                                                .Include(x => x.ICProductGroup)
                                                .Include(x => x.MESpecialist)
                                                .Where(x => !groupId.HasValue || x.FK_ICProductGroupID == groupId)
                                                .Where(x => !isShowWeb.HasValue || x.ICProductIsShowWeb == isShowWeb)
                                                .Where(x => (search == null || search.Trim() == string.Empty) || x.ICProductName.ToLower().Contains(search.ToLower()))
                                                .OrderBy(x => x.ICProductName)
                                                .Skip(page * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                var total = products.Count();
                var result = new PagedResult<ProductViewModel>
                {
                    Items = products.Select(x => x.ToProductViewModel()).ToArray(),
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

        public List<ShortProductDto> GetAll(bool isDetail)
        {
            try
            {
                var product = _productRepository.GetAll().Where(x=>x.ICProductIsDetail == isDetail).ToList();
                return product.Select(x => x.ToShortProductDto()).ToList();
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
