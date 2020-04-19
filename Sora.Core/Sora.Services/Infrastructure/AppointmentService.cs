using Sora.Common.CommonObjects;
using Sora.Common.Enums;
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
    public class AppointmentService : IAppointmentService
    {
        #region Properties
        private readonly ILogService _logService;
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<Appointment> _appointmentRepository => _unitOfWork.GetRepository<Appointment>();
        #endregion

        #region Constructor
        public AppointmentService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }
        #endregion

        #region Public methods
        public AppointmentViewModel Get(int id)
        {
            try
            {
                var appointment = _appointmentRepository.GetSingleById(id);
                return appointment.ToAppointmentViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Create(AppointmentViewModel dto)
        {
            try
            {
                var appointment = dto.ToAppointment();
                appointment.Status = AppointmentStatus.New.ToString();
                _appointmentRepository.Add(appointment);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Create(CreateAppointmentDto dto)
        {
            try
            {
                var appointment = dto.ToAppointment();
                appointment.Status = AppointmentStatus.New.ToString();
                _appointmentRepository.Add(appointment);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(AppointmentViewModel dto)
        {
            try
            {
                var appointment = _appointmentRepository.GetSingleById(dto.Id.GetValueOrDefault());
                appointment.CopyPropertiesFrom(dto);
                _appointmentRepository.Update(appointment);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatus(int id, string status)
        {
            try
            {
                var appointment = _appointmentRepository.GetSingleById(id);
                appointment.Status = status;
                _appointmentRepository.Update(appointment);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _appointmentRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ShortAppointmentDto> GetAll()
        {
            try
            {
                var result = _appointmentRepository.GetAll().Where(x => x.Status == AppointmentStatus.Processed.ToString()).ToList();
                return result.Select(x => x.ToShortAppointmentDto()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public PagedResult<AppointmentViewModel> Filter(int page, int pageSize, int? specialistId, string search = "")
        {
            try
            {
                var appointments = _appointmentRepository.GetAll()
                                    .Include(x=>x.MESpecialist)
                                    .Where(x => !specialistId.HasValue || x.FK_MESpecialistID == specialistId)
                                    .Where(x => (search == null || search.Trim() == string.Empty) ||
                                        (x.FirstName.ToLower().Contains(search) || x.LastName.ToLower().Contains(search) || x.Email.ToLower().Contains(search) || x.Phone.ToLower().Contains(search)))
                                    .OrderByDescending(x => x.AACreatedDate)
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .ToList();
                var total = appointments.Count();
                var result = new PagedResult<AppointmentViewModel>
                {
                    Items = appointments.Select(x => x.ToAppointmentViewModel()).ToArray(),
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
        #endregion
    }
}
