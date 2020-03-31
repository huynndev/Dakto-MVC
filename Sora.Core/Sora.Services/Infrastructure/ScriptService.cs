using Sora.EFCore.Infrastructure;
using Sora.Services.Abstractions;
using System;

namespace Sora.Services.Infrastructure
{
    public class ScriptService : IScriptService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogService _logService;
        #endregion

        #region Constructor
        public ScriptService(IUnitOfWork unitOfWork, ILogService logService)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
        }
        #endregion

        #region Public method
        /// <summary>
        /// Chạy script cho Database
        /// </summary>
        public void Run(string script)
        {
            try
            {
                _unitOfWork.ExecuteSqlCommand(script);
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
