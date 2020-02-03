using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sora.Hospital.Infrastructure.Constants
{
    public class UserConstants
    {
        public static List<SelectListItem> Role = new List<SelectListItem>
        {
            new SelectListItem { Text = "Quản lý nội dung (nội dung các trang, tinh tức, bài viết)", Value = "ContentManagement"},
            new SelectListItem { Text = "Quản lý thông tin liên hệ", Value = "ContactManagement"},
            new SelectListItem { Text = "Quản lý thông tin khách hàng trên web", Value = "CustomerInforManagement"},
            new SelectListItem { Text = "Quản lý thông tin Tư vấn", Value = "AdvisoryManagement"},
            new SelectListItem { Text = "Quản lý cấu hình chung, users và phân quyền", Value = "ConfigManagement"},
            new SelectListItem { Text = "Chưa cấp phép", Value = "NoPermisson"},
            new SelectListItem { Text = "Quản lý câu hỏi thường gặp", Value = "QuestionManagement"},
        };

        
    }
    public enum Role
    {
        ContentManagement,
        ContactManagement,
        CustomerInforManagement,
        AdvisoryManagement,
        ConfigManagement,
        NoPermisson,
        QuestionManagement
    }
}
