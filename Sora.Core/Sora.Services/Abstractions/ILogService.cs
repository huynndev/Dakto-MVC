using Sora.Entites.AD;
using System;
using System.Collections.Generic;

namespace Sora.Services.Abstractions
{
    public interface ILogService
    {
        void Create(Exception ex);

        IEnumerable<Log> GetAll();
    }
}
