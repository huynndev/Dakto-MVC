using Sora.EFCore.Infrastructure;
using Sora.EFCore.Repositories;
using Sora.Entites.IC;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.Infrastructure
{
    public class ContactMessageService : IContactMessageService
    {
        private IContactMessageRepository _contactMessageRepository;
        private IUnitOfWork _unitOfWork;

        public ContactMessageService(IContactMessageRepository contactMessageRepository, IUnitOfWork unitOfWork)
        {
            this._contactMessageRepository = contactMessageRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(ContactMessageViewModel message)
        {
            _contactMessageRepository.Add(ToContactMessage(message));
        }

        public ContactMessageViewModel GetContactMessageByID(int messageID)
        {
            ICContactMessage message = _contactMessageRepository.GetSingleById(messageID);
            return ToContactMessageViewModel(message);
        }

        public IEnumerable<ContactMessageViewModel> GetContactMessagePaging(int page, int pageSize, string sortOption, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public void CreateContactMessage(ContactMessageViewModel message)
        {

            Add(message);
            Save();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public ICContactMessage ToContactMessage(ContactMessageViewModel message)
        {
            return new ICContactMessage()
            {
                ICContactMessageSubject = message.ICContactMessageSubject,
                ICContactMessageContactName = message.ICContactMessageContactName,
                ICContactMessageContactEmail = message.ICContactMessageContactEmail,
                ICContactMessageContactPhone = message.ICContactMessageContactPhone,
                ICContactMessageContent = message.ICContactMessageContent
            };
        }

        public ContactMessageViewModel ToContactMessageViewModel(ICContactMessage message)
        {
            return new ContactMessageViewModel()
            {
                ICContactMessageSubject = message.ICContactMessageSubject,
                ICContactMessageContactName = message.ICContactMessageContactName,
                ICContactMessageContactEmail = message.ICContactMessageContactEmail,
                ICContactMessageContactPhone = message.ICContactMessageContactPhone,
                ICContactMessageContent = message.ICContactMessageContent
            };
        }
    }
}
