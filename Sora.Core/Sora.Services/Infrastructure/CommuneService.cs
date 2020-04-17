using Sora.Common.CommonObjects;
using Sora.Common.Extensions;
using Sora.EFCore.Infrastructure;
using Sora.Entites.GE;
using Sora.Services.Abstractions;
using Sora.Services.Infrastructure.Helpers;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Infrastructure
{
    public class CommuneService : ICommuneService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        private IRepository<Commune> _communeRepository => _unitOfWork.GetRepository<Commune>();
        #endregion

        #region Constructor
        public CommuneService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }
        #endregion

        #region Public methods

        public CommuneViewModel Get(int id)
        {
            try
            {
                var commune = _communeRepository.GetSingleById(id);
                return commune.ToCommuneViewModel();
            }
            catch (Exception ex)
            {
                _logService.Create(ex);
                throw ex;
            }
        }

        public void Create(CommuneViewModel dto)
        {
            try
            {
                var commune = dto.ToCommune();
                _communeRepository.Add(commune);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(CommuneViewModel dto)
        {
            try
            {
                var commune = _communeRepository.GetSingleById(dto.CommuneID.GetValueOrDefault());
                commune.CopyPropertiesFrom(dto);
                _communeRepository.Update(commune);
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
                _communeRepository.Delete(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedResult<CommuneViewModel> Filter(int page, int pageSize, string search = null)
        {
            try
            {
                var products = _communeRepository.GetAll()
                                                .Where(x=> (search == null || search.Trim() == string.Empty) || (x.Name.ToLower().Contains(search.ToLower()) || x.Address.ToLower().Contains(search.ToLower())))
                                                .OrderBy(x => x.Name)
                                                .Skip(page * pageSize)
                                                .Take(pageSize)
                                                .ToList();
                var total = products.Count();
                var result = new PagedResult<CommuneViewModel>
                {
                    Items = products.Select(x => x.ToCommuneViewModel()).ToArray(),
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
