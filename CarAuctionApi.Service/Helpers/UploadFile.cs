using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Helpers
{
    public static class UploadFile
    {
        public static string SaveImage(this IFormFile file, string path)
        {
            string fileName = Guid.NewGuid() + file.FileName;
            string filePath = Path.Combine(path, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileName;
        }
    }
}
