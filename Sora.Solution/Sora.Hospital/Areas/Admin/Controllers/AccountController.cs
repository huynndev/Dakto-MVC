using Newtonsoft.Json;
using Sora.Hospital.Infrastructure.Security;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sora.Hospital.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private static readonly log4net.ILog log
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            string websiteURL = "#";
            //try
            //{
            //    websiteURL = sAConfigurationRepository.GetConfigValue("site.domain");
            //}
            //catch (Exception)
            //{

            //}
            if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "nodata";
            TempData["ReturnUrl"] = returnUrl;
            TempData["websiteURL"] = websiteURL;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckLogin()
        {
            var form = Request.Form;
            string user = form["UserName"];
            string pass = form["Password"];
            AccountViewModel account = new AccountViewModel();
            bool isSuccess = _accountService.CheckAccountLogin(user, pass, out account);
            if (!isSuccess && account == null)
            {
                return Json(false);
            }
            else if (!account.ADUserIsActive ?? false)
            {
                return Json(false);
            }
            else
            {
                SaveUser(account);
                log.InfoFormat("{0} logged in at {1}", user, DateTime.Now.ToShortDateString());
                return Json(true);
            }
        }

        private void SaveUser(AccountViewModel account)
        {
            //List<SAUserPermission> userPemissions = new List<SAUserPermission>();
            //userPemissions = _userRepository.GetPermission(user.UserID);
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.UserId = account.ADUserID;
            serializeModel.FullName = account.ADUserName;
            serializeModel.Email = account.ADUserContactEmail;
            serializeModel.roles = account.ADUserRole;
            serializeModel.Avatar = account.ADUserAvatar;
            serializeModel.Permissions = new List<string>(); //userPemissions.Select(x => x.SAPermissionName).ToList<string>();
            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     account.ADUserName,
                     DateTime.Now,
                     DateTime.Now.AddHours(5),
                     false,
                     userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            Session["access_token"] = encTicket;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                Session.Abandon();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                log.ErrorFormat("{0} \n {1}", ex.Message, ex.StackTrace);
                return RedirectToAction("Login");
            }
        }
    }
}