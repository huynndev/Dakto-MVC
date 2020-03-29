using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Sora.Hospital.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        public static readonly log4net.ILog log
               = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ApiControllerBase()
        {
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
                log.Error(ex);
            }
            catch (DbUpdateException dbEx)
            {
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
                log.Error(dbEx);
            }
            catch (Exception ex)
            {
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                log.Error(ex);
            }
            return response;
        }
    }
}