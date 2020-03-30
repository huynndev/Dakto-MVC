using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface IDoctorService
    {
        DoctorViewModel Get(int id);

        void Create(DoctorViewModel dto);

        void Update(DoctorViewModel dto);

        void Delete(int id);

        List<DoctorViewModel> Filter(int page, int pageSize, out int total, int[] specialistIds, string search = null);

        List<ShortDoctorDto> GetAll();
    }
}
