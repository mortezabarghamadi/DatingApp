using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Extensions.Image;
using Application.Services.Interfaces;
using Domain.Entites.Photo;
using Domain.Entites.User;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Services.Implementation
{
    public class PhotoService:IPhotoService
    {
        private IUserRepository _userRepository;

        public PhotoService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Photo?> AddPhotoAsync(IFormFile file, int userId, string savePath)
        {
            if (file == null || !file.IsValidFile())
            {
                return null;
            }

            var user = await _userRepository.GetUserWithPhotos(userId);
            if (user == null) return null;

            // ذخیره فایل
            var fileName = await file.SaveFile(savePath);

            if (fileName == "File Not Found") return null;

            var photo = new Photo
            {
                Url = savePath.Replace("wwwroot", "") + "/" + fileName,
                IsMain = !user.Photos.Any(), // اگر کاربر عکسی ندارد، این عکس را اصلی قرار بده
                PublicId = Guid.NewGuid().ToString(),
                UserId = userId
            };

            user.Photos.Add(photo);
            _userRepository.UpdateUser(user); // به‌روزرسانی شیء کاربر (با این فرض که EF Core به صورت خودکار تغییرات لیست را پیگیری می‌کند)
            await _userRepository.SaveChangeAsync();

            return photo;
        }

        public async Task<bool> SetMainPhotoAsync(int userId, int photoId)
        {
            var user = await _userRepository.GetUserWithPhotos(userId);
            if (user == null|| !user.Photos.Any()) return false;
            var photoToSetMain = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photoToSetMain == null || photoToSetMain.IsMain) return false;

            // غیرفعال کردن عکس اصلی فعلی
            var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);
            if (currentMain != null) currentMain.IsMain = false;

            // تنظیم عکس جدید به عنوان اصلی
            photoToSetMain.IsMain = true;

             _userRepository.UpdateUser(user);
            await _userRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeletePhotoAsync(int userId, int photoId)
        {
            var user = await _userRepository.GetUserWithPhotos(userId); // یا متد شامل عکس‌ها
            if (user == null) return false;

            var photoToDelete = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photoToDelete == null) return false;

            // حذف فیزیکی فایل
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + photoToDelete.Url.Replace("/", "\\"));
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // حذف از دیتابیس
            user.Photos.Remove(photoToDelete);

            // اگر عکس اصلی حذف شد، یکی دیگر را به عنوان اصلی تنظیم کنید (اگر وجود داشته باشد)
            if (photoToDelete.IsMain && user.Photos.Any())
            {
                user.Photos.First().IsMain = true;
            }

            _userRepository.UpdateUser(user);
             await _userRepository.SaveChangeAsync();
             return true;
        }
    }
}
