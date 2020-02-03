using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Abstractions
{
    public interface IContactMessageService
    {
        void Add(ContactMessageViewModel message);

        void CreateContactMessage(ContactMessageViewModel message);

        ContactMessageViewModel GetContactMessageByID(int messageID);

        IEnumerable<ContactMessageViewModel> GetContactMessagePaging(int page, int pageSize, string sortOption, out int totalRow);

        void Save();
    }
}
