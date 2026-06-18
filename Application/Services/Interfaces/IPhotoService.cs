using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites.Photo;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces
{
    public interface IPhotoService
    {
        // آپلود یک عکس جدید برای کاربر
        Task<Photo?> AddPhotoAsync(IFormFile file, int userId, string savePath);
        // تنظیم یک عکس به عنوان عکس اصلی
        Task<bool> SetMainPhotoAsync(int userId, int photoId);
        // حذف یک عکس
        Task<bool> DeletePhotoAsync(int userId, int photoId);
    }
}
