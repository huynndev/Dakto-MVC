using Sora.Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sora.Services.Abstractions
{
    public interface ISpecialistService
    {
        void Create(SpecialistViewModel dto);

        SpecialistViewModel Get(int id);

        SpecialistViewModel Update(SpecialistViewModel dto);

        List<SpecialistViewModel> GetAll();

        void Delete(int id);

        List<SpecialistViewModel> GetSpecialistsByDoctor(int doctorId);
    }
}
