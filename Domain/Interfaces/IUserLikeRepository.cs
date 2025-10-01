using Domain.DTOs.UserLike;
using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserLikeRepository
    {
        //عدم لایک تکراری
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        // یک شیء User را به همراه اطلاعات لایک‌هایی که آن کاربر انجام داده است، برمی‌گرداند.
        Task<User> GetUserWithLikes(int userId);
        //فهرستی از کاربرانی که توسط کاربر فعلی لایک شده‌اند یا کاربرانی که کاربر فعلی را لایک کرده‌اند 
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
        //اضافه کردن تغییرات
        Task InsertAsync(UserLike userLike);
        //ذخیره اطلاعات
        Task SaveAsync();
    }
}
