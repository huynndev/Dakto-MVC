using System.Collections.Generic;
using System.Security.Principal;

namespace Sora.Hospital.Infrastructure.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (role != null && roles != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string roles { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string ERPCustomerId { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public int NotifyCount { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();
    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string roles { get; set; }
        public string Avatar { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();
    }

    public class CustomPrincipalSerializeCustomerModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string ERPCustomerId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; }
    }
}