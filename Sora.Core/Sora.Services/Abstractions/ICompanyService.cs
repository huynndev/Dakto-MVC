using Sora.Entites.CS;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Abstractions
{
    public interface ICompanyService
    {
        void Update(CompanyViewModel company);

        CompanyViewModel GetCompanyInfo();

        void SaveChanges();
    }
}
