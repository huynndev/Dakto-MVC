using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface ISpecialistTypeService
    {
        void Create(SpecialistTypeViewModel dto);

        SpecialistTypeViewModel Get(int id);

        SpecialistTypeViewModel Update(SpecialistTypeViewModel dto);

        List<SpecialistTypeViewModel> GetAll();

        List<SpecialistTypeDetailDto> GetAllDetail();

        void Delete(int id);
    }
}
