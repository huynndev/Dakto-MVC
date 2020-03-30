using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.AD;
using Sora.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sora.Services.Infrastructure
{
    public class LogService : ILogService
    {
        private ILogRepository _logRepository;
        private IUnitOfWork _unitOfWork;

        public LogService(ILogRepository logRepository, IUnitOfWork unitOfWork)
        {
            this._logRepository = logRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Exception ex)
        {
            Log log = new Log
            {
                LogMessage = ex.ToString(),
                AACreatedDate = DateTime.Now
            };
            _logRepository.Add(log);
            _unitOfWork.Commit();
        }

        public IEnumerable<Log> GetAll()
        {
            return _logRepository.GetAll().OrderByDescending(x => x.AACreatedDate);
        }
    }
}
