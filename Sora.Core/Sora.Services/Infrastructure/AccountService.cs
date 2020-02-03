using Sora.Common.Extensions;
using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.AD;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Infrastructure
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public AccountService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool CheckAccountLogin(string username, string password, out AccountViewModel account)
        {
            string hasPasswordMD5 = password.ToHashMD5();
            ADUser user = _userRepository.GetSingleByCondition(o => o.ADUserName == username && o.ADPassword == hasPasswordMD5);
            account = ToAccountViewModel(user);
            return account == null ? false : true;
        }

        public AccountViewModel ToAccountViewModel(ADUser user)
        {
            if (user == null)
                return null;

            return new AccountViewModel()
            {
                ADUserName = user.ADUserName,
                ADUserIsActive = user.ADUserIsActive
            };
        }
    }
}
