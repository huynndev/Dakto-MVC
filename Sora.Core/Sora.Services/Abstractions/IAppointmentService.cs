using Sora.Common.CommonObjects;
using Sora.Services.ViewModels;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface IAppointmentService
    {
        AppointmentViewModel Get(int id);

        void Create(AppointmentViewModel dto);

        void Create(CreateAppointmentDto dto);

        void Update(AppointmentViewModel dto);

        void UpdateStatus(int id, string status);

        void Delete(int id);

        List<ShortAppointmentDto> GetAll();

        PagedResult<AppointmentViewModel> Filter(int page, int pageSize, int? specialistId, string search = "");
    }
}
