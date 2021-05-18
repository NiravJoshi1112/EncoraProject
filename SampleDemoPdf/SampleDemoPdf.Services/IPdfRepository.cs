using SampleDemoPdf.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDemoPdf.Services
{
    public interface IPdfRepository
    {
        Task<List<UserViewModel>> GetUserDetailList();
        Task<bool> SavePdfData(int userId, string fileName, byte[] fileData);
        Task<List<UserFileViewModel>> GetUserFileList();
    }
}
