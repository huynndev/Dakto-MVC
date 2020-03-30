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
using System.Threading.Tasks;

namespace Sora.Services.Infrastructure
{
    public class SpecialistService : ISpecialistService
    {
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private readonly IDoctorSpecialistRepository _doctorSpecialistRepository;

        public SpecialistService(ISpecialistRepository specialistRepository,
            IUnitOfWork unitOfWork,
            ILogService logService,
            IDoctorSpecialistRepository doctorSpecialistRepository)
        {
            _specialistRepository = specialistRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
            _doctorSpecialistRepository = doctorSpecialistRepository;
        }

        public void Create(SpecialistViewModel dto)
        {
            try
            {
                _specialistRepository.Add(dto.ToMESpecialist());
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }

        }

        public SpecialistViewModel Get(int id)
        {
            try
            {
                var result = _specialistRepository.GetAll().Include(x => x.MEDoctor).Include(x => x.MEDoctorSpecialists).FirstOrDefault(x => x.MESpecialistID == id);
                if (result == null)
                {
                    throw new Exception("Object not found!");
                }
                return result.ToSpecialistViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public SpecialistViewModel Update(SpecialistViewModel dto)
        {
            try
            {
                var entity = _specialistRepository.GetSingleById(dto.MESpecialistID.GetValueOrDefault());
                if (entity == null)
                {
                    throw new Exception("Object not found!");
                }
                entity.CopyPropertiesFrom(dto);

                _specialistRepository.Update(entity);
                _unitOfWork.Commit();

                return Get(dto.MESpecialistID.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<SpecialistViewModel> GetAll()
        {
            try
            {
                var result = _specialistRepository.GetAll().Include(x => x.MEDoctorSpecialists).Include(x => x.MEDoctor).OrderBy(x => x.MESpecialistName).ToList();
                return result.Select(x => x.ToSpecialistViewModel()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<SpecialistViewModel> GetSpecialistsByDoctor(int doctorId)
        {
            try
            {
                var result = _doctorSpecialistRepository.GetAll()
                            .Include(x => x.MESpecialist)
                            .Where(x => x.FK_MEDoctorID == doctorId)
                            .ToList();
                return result.Select(x => x.ToSpecialistViewModel()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _specialistRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }
    }
}
