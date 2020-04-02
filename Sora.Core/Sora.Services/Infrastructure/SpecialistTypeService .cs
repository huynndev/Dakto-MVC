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
    public class SpecialistTypeService : ISpecialistTypeService
    {
        private readonly ISpecialistTypeRepository _specialistTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;

        public SpecialistTypeService(ISpecialistTypeRepository specialistTypeRepository,
            IUnitOfWork unitOfWork,
            ILogService logService)
        {
            _specialistTypeRepository = specialistTypeRepository;
            _unitOfWork = unitOfWork;
            _logService = logService;
        }

        public void Create(SpecialistTypeViewModel dto)
        {
            try
            {
                _specialistTypeRepository.Add(dto.ToMESpecialistType());
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }

        }

        public SpecialistTypeViewModel Get(int id)
        {
            try
            {
                var result = _specialistTypeRepository.GetAll().FirstOrDefault(x => x.MESpecialistTypeID == id);
                if (result == null)
                {
                    throw new Exception("Object not found!");
                }
                return result.ToSpecialistTypeViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public SpecialistTypeViewModel Update(SpecialistTypeViewModel dto)
        {
            try
            {
                var entity = _specialistTypeRepository.GetSingleById(dto.MESpecialistTypeID.GetValueOrDefault());
                if (entity == null)
                {
                    throw new Exception("Object not found!");
                }
                entity.CopyPropertiesFrom(dto);

                _specialistTypeRepository.Update(entity);
                _unitOfWork.Commit();

                return Get(dto.MESpecialistTypeID.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<SpecialistTypeViewModel> GetAll()
        {
            try
            {
                var result = _specialistTypeRepository.GetAll().ToList();
                return result.Select(x => x.ToSpecialistTypeViewModel()).ToList();
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
                _specialistTypeRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public List<SpecialistTypeDetailDto> GetAllDetail()
        {
            try
            {
                var result = _specialistTypeRepository.GetAll().Include(x => x.MESpecialists).ToList();
                return result.Select(x => x.ToSpecialistTypeDetailDto()).ToList();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }
    }
}
