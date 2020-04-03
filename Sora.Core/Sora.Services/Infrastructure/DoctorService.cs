using Sora.Common.Extensions;
using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.ME;
using Sora.Services.Abstractions;
using Sora.Services.Infrastructure.Helpers;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sora.Services.Infrastructure
{
    public class DoctorService : IDoctorService
    {
        #region Properties
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private readonly IDoctorSpecialistRepository _doctorSpecialistRepository;
        private readonly ISpecialistService _specialistService;
        #endregion

        #region Constructor
        public DoctorService(IDoctorRepository doctorRepository,
            IUnitOfWork unitOfWork,
            ILogService logService,
            IDoctorSpecialistRepository doctorSpecialistRepository,
            ISpecialistService specialistService)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
            _doctorSpecialistRepository = doctorSpecialistRepository;
            _specialistService = specialistService;
        }
        #endregion

        #region Public Method
        public void Create(DoctorViewModel dto)
        {
            try
            {
                var doctor = _doctorRepository.Add(dto.ToMEDoctor());
                UpdateDoctorIntoSpecialist(doctor.MEDoctorID, dto.MESpecialistIds.Select(x => int.Parse(x)).ToArray());

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
                _doctorSpecialistRepository.DeleteMulti(x => x.FK_MEDoctorID == id);
                _doctorRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoctorViewModel Get(int id)
        {
            try
            {
                var doctor = _doctorRepository.GetSingleById(id);
                if (doctor == null)
                {
                    throw new Exception("Object not found!");
                }

                return doctor.ToDoctorViewModel(_specialistService.GetSpecialistsByDoctor(doctor.MEDoctorID).ToArray());
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<DoctorViewModel> Filter(int page, int pageSize, out int total, int[] specialistIds, string search = null)
        {
            try
            {
                List<DoctorViewModel> doctors = new List<DoctorViewModel>();
                if (!specialistIds.IsNullOrEmpty())
                {
                    doctors = _doctorSpecialistRepository.GetAll()
                                .Include(x => x.MEDoctor)
                                .Where(x => specialistIds.Any(y => y == x.FK_MESpecialistID))
                                .Where(x => (search == null || search.Trim() == string.Empty) || x.MEDoctor.MEDoctorName.ToLower().Contains(search.ToLower()))
                                .ToList()
                                .OrderBy(x => x.MEDoctor.MEDoctorName)
                                .Skip(page - 1 * pageSize)
                                .Take(pageSize)
                                .Select(x => x.ToDoctorViewModel(_specialistService.GetSpecialistsByDoctor(x.FK_MEDoctorID.GetValueOrDefault()).ToArray()))
                                .ToList();
                }
                else
                {
                    doctors = _doctorRepository.GetAll()
                                .Where(x => (search == null || search.Trim() == string.Empty) || x.MEDoctorName.ToLower().Contains(search.ToLower()))
                                .ToList()
                                .OrderBy(x => x.MEDoctorName)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .Select(x => x.ToDoctorViewModel(_specialistService.GetSpecialistsByDoctor(x.MEDoctorID).ToArray()))
                                .ToList();
                }

                total = doctors.Count();
                return doctors;
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<ShortDoctorDto> GetAll()
        {
            try
            {
                var doctors = _doctorRepository.GetAll().ToList();
                return doctors.Select(x => x.ToShortDoctorDto()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Update(DoctorViewModel dto)
        {
            try
            {
                var doctor = _doctorRepository.GetSingleById(dto.MEDoctorID.GetValueOrDefault());
                if (doctor == null)
                {
                    throw new Exception("Object not found!");
                }

                doctor.CopyPropertiesFrom(dto);
                _doctorRepository.Update(doctor);

                UpdateDoctorIntoSpecialist(doctor.MEDoctorID, dto.MESpecialistIds.Select(x => int.Parse(x)).ToArray());

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Private method
        private void UpdateDoctorIntoSpecialist(int doctorId, int[] specialistIds)
        {
            var specialListsFromDb = _doctorSpecialistRepository.GetAll().Where(x => x.FK_MEDoctorID == doctorId).ToList();

            var recordToDelete = specialListsFromDb.Where(x => !specialistIds.Any(y => y == x.FK_MESpecialistID));
            if (!recordToDelete.IsNullOrEmpty())
            {
                _doctorSpecialistRepository.DeleteMulti(x => recordToDelete.Any(y => y.FK_MESpecialistID == x.FK_MESpecialistID));
            }

            var recordToInsert = specialistIds.Where(x => !specialListsFromDb.Any(y => y.FK_MESpecialistID == x));

            if (!recordToInsert.IsNullOrEmpty())
            {
                foreach (var item in recordToInsert)
                {
                    _doctorSpecialistRepository.Add(new MEDoctorSpecialist { FK_MEDoctorID = doctorId, FK_MESpecialistID = item });
                }
            }
        }
        #endregion
    }
}
