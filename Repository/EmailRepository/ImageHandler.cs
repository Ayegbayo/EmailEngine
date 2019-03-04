using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Repository
{
    public class FileHandler:IFileHandler
    {
        public Image FetchImage(int? width, int? height, string fileName)
        {
            try
            {
                var fileDirectory = $"{FileType.PICTURE.ToString().ToLower()}s";

                var newPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory, fileName);
                var img = Image.FromFile(newPath);

                if (width.HasValue && height.HasValue)
                {
                    return new Bitmap(img, new Size(width.Value, height.Value));
                }

                return img;
            }
            catch(Exception)
            {
                return null;
            }
            
        }
        public async Task<string> UploadFile(IFormFile file, FileType fileType)
        {
            try
            {
                if (file != null)
                {                    
                    var extension = file.FileName.Split('.')[1];

                    var fileName = Guid.NewGuid ().ToString();
                    fileName += "."+extension;

                    var fileDirectory = $"{fileType.ToString().ToLower()}s";                                   

                    var newPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory, fileName);
                    using (var fs = new FileStream(newPath, FileMode.Create))
                    {
                       await  file.CopyToAsync(fs);
                    }

                    return fileName;
                }

                return null;
            }
            catch(Exception ex)
            {
                var ec = ex;
                return null;
            }           
        }
    }
}
