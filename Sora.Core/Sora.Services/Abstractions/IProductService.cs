using Sora.Common.CommonObjects;
using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface IProductService
    {
        ProductViewModel Get(int id);

        void Create(ProductViewModel dto);

        void Update(ProductViewModel dto);

        void Delete(int id);

        PagedResult<ProductViewModel> Filter(int page, int pageSize, int? groupId, bool? isShowWeb, string search = null);

        List<ShortProductDto> GetAll(bool isDetail);
    }
}
