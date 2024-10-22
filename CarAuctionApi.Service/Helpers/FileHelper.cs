using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Helpers
{
    public class FileHelper
    {
        public static bool IsCorrectFileType(IFormFile file, string content)
        {
            return file.ContentType.Contains(content);
        }

        public static bool IsSizeCorrect(IFormFile file, double sizeMb)
        {
            return (double)file.Length / 1024 / 1024 <= sizeMb;
        }

        public static bool RemoveImage(string path)
        {
            if (!File.Exists(path))
                return false;

            File.Delete(path);
            return true;
        }
    }
}
