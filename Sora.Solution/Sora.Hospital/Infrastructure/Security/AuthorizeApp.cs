using Newtonsoft.Json;
using Sora.Hospital.Infrastructure.Constants;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Sora.Hospital.Infrastructure.Security
{
    public class AuthorizeApp : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string access_token = (httpContext.Session["access_token"] != null) ? httpContext.Session["access_token"].ToString() : null;

            if (!string.IsNullOrWhiteSpace(access_token))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(access_token);
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserId = serializeModel.UserId;
                newUser.FullName = serializeModel.FullName;
                newUser.Email = serializeModel.Email;
                newUser.roles = serializeModel.roles;
                newUser.Avatar = serializeModel.Avatar != null ? serializeModel.Avatar : "";
                newUser.Permissions = serializeModel.Permissions;
                HttpContext.Current.User = newUser;
                return true;
            }
            return false;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                            new
                            {
                                area = "Admin",
                                controller = "Account",
                                action = "Login",
                                returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                            }));
            }
        }
    }
    public class AuthorizeWeb : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string access_token = (httpContext.Session["access_token"] != null)
                ? httpContext.Session["access_token"].ToString()
                : (httpContext.Request.Cookies["miracle_login"] != null ? httpContext.Request.Cookies["miracle_login"].Value : null);

            if (!string.IsNullOrWhiteSpace(access_token))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(access_token);
                CustomPrincipalSerializeCustomerModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeCustomerModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.UserId = serializeModel.UserId;
                newUser.FullName = serializeModel.FullName;
                newUser.Phone = serializeModel.Phone;
                newUser.Email = serializeModel.Email;
                newUser.Avatar = serializeModel.Avatar != null ? serializeModel.Avatar : "";
                newUser.ERPCustomerId = serializeModel.ERPCustomerId;
                newUser.Address = serializeModel.Address;
                newUser.City = serializeModel.City;
                newUser.District = serializeModel.District;

                HttpContext.Current.User = newUser;
            }
            return true;

        }
    }

    public class AuthorizeLogin : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //string access_token = (httpContext.Request.Cookies["app_baongocland"] != null) ? httpContext.Request.Cookies["app_baongocland"].Value : null;
            string access_token = (httpContext.Session["access_token"] != null) ? httpContext.Session["access_token"].ToString() : null;

            if (!string.IsNullOrWhiteSpace(access_token))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(access_token);
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                if (!string.IsNullOrWhiteSpace(serializeModel.roles))
                    return true;
            }
            return false;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "Login"
                                //action = "Login",
                                //returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                            }));
            }
        }
    }

    public class AuthorizeStop : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //SettingRepository _SettingRepository = new SettingRepository();
            //var check = _SettingRepository.GetMultiKey("StopWeb");
            //if (check.Count > 0 && !string.IsNullOrWhiteSpace(check[0].SettingValue) && !(Convert.ToBoolean(check[0].SettingValue)))
            //{
            //    return false;
            //}
            return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "PageNotFound",
                            action = "StopWed"
                        }));
            }
        }
    }
}