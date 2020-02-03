using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.CS;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Infrastructure
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _companyRepository;
        private IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._companyRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        public CompanyViewModel GetCompanyInfo()
        {
            var company = _companyRepository.GetAll().FirstOrDefault();
            if (company == null)
                return null;

            CompanyViewModel defaultConpany = new CompanyViewModel()
            {
                CSCompanyID = company.CSCompanyID,
                CSCompanyName = company.CSCompanyName,
                CSCompanyAddress = company.CSCompanyAddress,
                CSCompanyEmail = company.CSCompanyEmail,
                CSCompanyPhone = company.CSCompanyPhone,
                CSCompanyWebsite = company.CSCompanyWebsite,
                CSCompanyAvatar = company.CSCompanyAvatar,
                CSCompanyDesc = company.CSCompanyDesc,
                CSCompanyLang = company.CSCompanyLang
            };
            return defaultConpany;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(CSCompany post)
        {
            _companyRepository.Update(post);
            SaveChanges();
        }

        public void Update(CompanyViewModel company)
        {
            CSCompany companyInfo = ToCSCompany(company);
            Update(companyInfo);
        }

        public CSCompany ToCSCompany(CompanyViewModel model)
        {
            if (model == null)
                return null;

            return new CSCompany()
            {
                CSCompanyID = model.CSCompanyID,
                CSCompanyName = model.CSCompanyName,
                CSCompanyAddress = model.CSCompanyAddress,
                CSCompanyEmail = model.CSCompanyEmail,
                CSCompanyPhone = model.CSCompanyPhone,
                CSCompanyWebsite = model.CSCompanyWebsite,
                CSCompanyAvatar = model.CSCompanyAvatar,
                CSCompanyDesc = model.CSCompanyDesc,
                CSCompanyLang = model.CSCompanyLang
            };
        }
    }
}
