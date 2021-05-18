using SampleDemoPdf.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleDemoPdf.Entity;
using System.Data.Entity;
using AutoMapper;
using SampleDemoPdf.Services;

namespace SampleDemoPdf.Repository
{
    public class PdfRepository: IPdfRepository
    {
        public async Task<List<UserViewModel>> GetUserDetailList()
        {
            using (var db= new SamplePdfDbEntities())
            {
                var userList = await db.UserDetails.ToListAsync();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<UserDetail, UserViewModel>();
                });
                IMapper mapper = config.CreateMapper();

                return mapper.Map<List<UserDetail>, List<UserViewModel>>(userList);
            }
        }

        public async Task<bool> SavePdfData(int userId,string fileName,byte[] fileData)
        {
            try
            {
                using (var db = new SamplePdfDbEntities())
                {
                    FileData data = new FileData();
                    data.UserId = userId;
                    data.FileDataByte = fileData;
                    data.FileName = fileName;
                    db.FileDatas.Add(data);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserFileViewModel>> GetUserFileList()
        {
            using (var db = new SamplePdfDbEntities())
            {
                return await (from u in db.UserDetails
                              join f in db.FileDatas on u.UserId equals f.UserId
                              select new UserFileViewModel
                              {
                                  UserName = u.UserName,
                                  UserId = u.UserId,
                                  FileName = f.FileName,
                                  FileId = f.FileId,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName
                              }).ToListAsync();
            }
        }
    }
}
