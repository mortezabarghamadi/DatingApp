using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions.Image
{
    public static class SaveFileInServer
    {
        public static async Task<string> SaveFile(this IFormFile inputTarget, string savePath)
        {
            if (inputTarget == null) return "File Not Found";
            var fileName = Guid.NewGuid() + inputTarget.FileName;


            var folderName = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            var path = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await inputTarget.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
