using Sora.Common.Constants;
using Sora.Hospital.Infrastructure.Core;
using Sora.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Sora.Hospital.Controllers
{
    [RoutePrefix("api/document")]
    public class DocumentsController : ApiControllerBase
    {
        #region Properties
        private readonly IDocumentService _documentService;
        #endregion

        #region Constructor
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Lấy danh sách văn bản
        /// </summary>
        [HttpGet]
        [Route("filter/{page}/{pageSize}")]
        public HttpResponseMessage Filter(int page, int pageSize, int? typeId, string search)
        {
            var result = _documentService.FilterDocument(page, pageSize, typeId, search);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết văn bản
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var result = _documentService.GetDocument(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Lấy danh sách loại văn bản
        /// </summary>
        [HttpGet]
        [Route("types")]
        public HttpResponseMessage GetTypes()
        {
            var result = _documentService.GetAllDocumentType();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Tải file đính kèm trong văn bản
        /// </summary>
        [HttpGet]
        [Route("download-file")]
        public HttpResponseMessage DownloadFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string fullPath = AppDomain.CurrentDomain.BaseDirectory + Constants.PATH_IMAGE_DOCUMENT + fileName;
                if (File.Exists(fullPath))
                {

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(fullPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    return response;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        #endregion
    }
}
