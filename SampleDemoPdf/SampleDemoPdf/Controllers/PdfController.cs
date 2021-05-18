using SampleDemoPdf.Services;
using SampleDemoPdf.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SampleDemoPdf.Controllers
{
    public class PdfController : ApiController
    {
        private readonly IPdfRepository _pdfRepository;

        public PdfController(IPdfRepository pdfRepository)
        {
            this._pdfRepository = pdfRepository;
        }

        // GET: api/Pdf/GetUserList
        [HttpGet]
        [Route("api/pdf/GetUserList")]
        public async Task<IEnumerable<UserViewModel>> GetUserList()
        {
            return await this._pdfRepository.GetUserDetailList();
        }

        // Post: api/Pdf/SavePdfData
        [HttpPost]
        [Route("api/pdf/SavePdfData")]
        public async Task<bool> SavePdfData(int userId)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var files = HttpContext.Current.Request.Files["UploadedFile"];
                if(files != null)
                {
                    String FileExt = Path.GetExtension(files.FileName).ToUpper();

                    if (FileExt == ".PDF")
                    {
                        Stream str = files.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] fileData = Br.ReadBytes((Int32)str.Length);

                        var isUpload = await this._pdfRepository.SavePdfData(userId, files.FileName, fileData);
                        return isUpload;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
            //int lenght = Request.File.PostedFile.ContentLength;
            //byte[] data = new byte[lenght];
            //FileUpload1.PostedFile.InputStream.Read(data, 0, lenght);
            //return "value";
        }

        [HttpGet]
        [Route("api/pdf/GetUserFileList")]
        public async Task<List<UserFileViewModel>> GetUserFileList()
        {
            return await this._pdfRepository.GetUserFileList();
        }
    }
}
