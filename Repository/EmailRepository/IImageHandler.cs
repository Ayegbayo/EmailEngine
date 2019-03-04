using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Drawing;
using EmailEngine.Repository.Interface;

namespace EmailEngine.Repository
{
    public interface IFileHandler:IAutoDependencyRegister
    {
        Task<string> UploadFile(IFormFile file, FileType fileType);
        Image FetchImage(int? width, int? height, string fileName);
    }

    public enum FileType
    {
        PICTURE = 1,
        VIDEOS
    }
}
