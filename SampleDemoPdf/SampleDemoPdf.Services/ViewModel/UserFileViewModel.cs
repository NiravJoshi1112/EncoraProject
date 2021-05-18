using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemoPdf.Services.ViewModel
{
    public class UserFileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FileName { get; set; }
        public int FileId { get; set; }
        public byte[] fileDataByte { get; set; }
    }
}
