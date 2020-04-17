using Sora.Common.CommonObjects;
using Sora.Services.ViewModels;

namespace Sora.Services.Abstractions
{
    public interface ICommuneService
    {
        CommuneViewModel Get(int id);

        void Create(CommuneViewModel dto);

        void Update(CommuneViewModel dto);

        void Delete(int id);

        PagedResult<CommuneViewModel> Filter(int page, int pageSize, string search = null);
    }
}
