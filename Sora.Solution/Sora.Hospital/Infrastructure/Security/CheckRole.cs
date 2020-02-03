using Newtonsoft.Json;
using Sora.Hospital.Infrastructure.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Sora.Hospital.Infrastructure.Security
{
    public class CheckRole : ActionFilterAttribute
    {
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var result = CheckRoles(filterContext);
            if (!CheckRoles(filterContext))
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "NotFound"
                            }));
            }
        }

        protected bool CheckRoles(ActionExecutingContext filterContext)
        {
            string access_token = (HttpContext.Current.Session["access_token"] != null) ? HttpContext.Current.Session["access_token"].ToString() : null;

            if (!string.IsNullOrWhiteSpace(access_token))
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(access_token);
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                if (!string.IsNullOrWhiteSpace(serializeModel.roles) && UserConstants.Role.Count(x => x.Value == serializeModel.roles && x.Value != Role.NoPermisson.ToString()) > 0)
                {
                    var currentRoles = serializeModel.Permissions;
                    List<string> rqsRoles = Roles.Split(',').ToList();

                    var result = rqsRoles.Where(x => currentRoles.Any(y => x.Contains(y)));
                    return result.Count() > 0;
                }
                return false;
            }
            return false;
        }
    }
}