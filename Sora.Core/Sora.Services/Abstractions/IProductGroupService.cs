using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface IProductGroupService
    {
        ProductGroupViewModel Get(int id);

        void Create(ProductGroupViewModel dto);

        void Update(ProductGroupViewModel dto);

        void Delete(int id);

        List<ProductGroupViewModel> GetAll();
    }
}
