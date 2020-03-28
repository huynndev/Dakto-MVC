using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/contact-message")]
    public class ContactMessageController : ApiControllerBase
    {
        private IContactMessageService _contactMessageService;
        public ContactMessageController(IContactMessageService contactMessageService) 
            : base()
        {
            this._contactMessageService = contactMessageService;
        }

        /// <summary>
        /// Tạo mới liên hệ
        /// </summary>
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage CreateContactMessage(ContactMessageViewModel contactMessage)
        {
            try
            {
                _contactMessageService.CreateContactMessage(contactMessage);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
