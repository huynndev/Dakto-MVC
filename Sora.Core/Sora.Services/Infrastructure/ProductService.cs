using Newtonsoft.Json;
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
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Script.Serialization;

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
            ICProduct product = new ICProduct();
            try
            {
                var maxId = _productRepository.GetAll().OrderByDescending(x => x.ICProductID).FirstOrDefault()?.ICProductID;
                product = dto.ToICProduct();
                product.ICProductID = maxId.GetValueOrDefault() + 1;
                JsonChildProductDto[] childs = JsonConvert.DeserializeObject<JsonChildProductDto[]>(dto.ChildProductString);
                product.ICProductIsDetail = !childs.IsNullOrEmpty();
                product = _productRepository.Add(dto.ToICProduct());
                _unitOfWork.Commit();

                CreateOrUpdateProductDetail(childs, product.ICProductID);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _productRepository.Delete(product.ICProductID);
                _unitOfWork.Commit();
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
                throw ex;
            }
        }

        public ProductViewModel Get(int id)
        {
            try
            {
                var product = _productRepository.GetAll()
                    .Include(x => x.ICProductGroup)
                    .Include(x => x.MESpecialist)
                    .FirstOrDefault(x => x.ICProductID == id);
                if (product == null)
                {
                    throw new Exception("Object not found!");
                }
                var result = product.ToProductViewModel();

                var childProducts = _productDetailRepository.GetAll().Include(x=>x.ICProduct).Where(x => x.FK_ICProductParentID == id).ToList();
                if (!childProducts.IsNullOrEmpty())
                {
                    result.ProductDetails = childProducts.Select(x => x.ToProductDetailDto()).ToArray();
                    result.ChildProductString = JsonConvert.SerializeObject(childProducts.Select(x => new JsonChildProductDto { id = x.FK_ICProductID.GetValueOrDefault(), quantity = (int)x.ICProductDetailQty }));
                }
                return result;
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
                JsonChildProductDto[] childs = JsonConvert.DeserializeObject<JsonChildProductDto[]>(dto.ChildProductString);
                product.ICProductIsDetail = !childs.IsNullOrEmpty();
                _productRepository.Update(product);
                CreateOrUpdateProductDetail(childs, product.ICProductID);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PagedResult<ProductViewModel> Filter(int page, int pageSize, int? groupId, bool? isShowWeb, string productType, string search = null)
        {
            try
            {
                var products = _productRepository.GetAll()
                                                .Include(x => x.ICProductGroup)
                                                .Include(x => x.MESpecialist)
                                                .Where(x => !groupId.HasValue || x.FK_ICProductGroupID == groupId)
                                                .Where(x => !isShowWeb.HasValue || x.ICProductIsShowWeb == isShowWeb)
                                                .Where(x => (search == null || search.Trim() == string.Empty) || x.ICProductName.ToLower().Contains(search.ToLower()))
                                                .Where(x => (productType == null || productType.Trim() == string.Empty) || x.ICProductType.Equals(productType))
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

        public List<ShortProductDto> GetAll(bool? isDetail = null)
        {
            try
            {
                var product = _productRepository.GetAll().Where(x => !isDetail.HasValue || x.ICProductIsDetail == isDetail).ToList();
                return product.Select(x => x.ToShortProductDto()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }
        #endregion

        #region Private methods
        private void CreateOrUpdateProductDetail(JsonChildProductDto[] dto, int productId)
        {
            var productDetails = _productDetailRepository.GetAll().Where(x => x.FK_ICProductParentID == productId).ToList();
            foreach (var item in dto)
            {
                var oldProductDetail = productDetails.FirstOrDefault(x => x.FK_ICProductID == item.id);
                if (oldProductDetail == null)
                {
                    var product = _productRepository.GetAll().FirstOrDefault(x => x.ICProductID == item.id);
                    if (product == null)
                    {
                        throw new Exception("Product not found!");
                    }
                    ICProductDetail detail = new ICProductDetail();
                    detail.FK_ICProductID = product.ICProductID;
                    detail.ICProductDetailPrice = product.ICProductPrice;
                    detail.FK_ICProductParentID = productId;
                    detail.ICProductDetailTotalAmout = product.ICProductPrice * item.quantity;
                    detail.ICProductDetailQty = item.quantity;
                    _productDetailRepository.Add(detail);
                }
                else
                {
                    if (item.quantity != oldProductDetail.ICProductDetailQty)
                    {
                        oldProductDetail.ICProductDetailQty = item.quantity;
                        oldProductDetail.ICProductDetailTotalAmout = oldProductDetail.ICProductDetailPrice * item.quantity;
                        _productDetailRepository.Update(oldProductDetail);
                    }
                }
            }
            var productDetailsToDelete = productDetails.Where(x => !dto.Any(y => y.id == x.FK_ICProductID));
            if (!productDetailsToDelete.IsNullOrEmpty())
            {
                foreach (var item in productDetailsToDelete)
                {
                    _productDetailRepository.Delete(item.ICProductDetailID);
                }
            }
        }
        #endregion
    }
}
