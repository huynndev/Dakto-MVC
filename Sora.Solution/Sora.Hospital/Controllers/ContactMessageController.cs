using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using Sora.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("Api/ContactMessage")]
    public class ContactMessageController : ApiControllerBase
    {
        private IContactMessageService _contactMessageService;
        public ContactMessageController(IContactMessageService contactMessageService) 
            : base()
        {
            this._contactMessageService = contactMessageService;
        }

        [HttpPost]
        [Route("CreateContactMessage")]
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
