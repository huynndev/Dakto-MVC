using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.ViewModels
{
    public class AccountViewModel
    {
        public int ADUserID { get; set; }

        public string ADUserName { get; set; }

        public string ADPassword { get; set; }

        public string ADUserDisplayName { get; set; }

        public string ADUserContactEmail { get; set; }

        public string ADUserAvatar { get; set; }

        public string ADUserRole { get; set; }

        public bool? ADUserIsActive { get; set; }
    }
}
