using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Abstractions
{
    public interface IAccountService
    {
        bool CheckAccountLogin(string username, string password, out AccountViewModel account);
    }
}
