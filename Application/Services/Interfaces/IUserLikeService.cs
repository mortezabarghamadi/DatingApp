using Domain.DTOs.UserLike;
using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserLikeService
    {
        //عدم لایک تکراری
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        //گرفتن کاربر خاص با لایک هایش  
        Task<User> GetUserWithLikes(int userId);
        //	لیست افرادی را که کاربر لایک کرده است یا توسط آن‌ها لایک شده است
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
        //عملیات ثبت لایک را انجام می‌دهد 
        Task<bool> AddUserLikeAsync(int sourceUserId, string userName);
    }
}
