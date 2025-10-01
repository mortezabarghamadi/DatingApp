using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Domain.DTOs.UserLike;
using Domain.Entites.User;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserLikeRepository:IUserLikeRepository
    {
        private readonly DatingAppContext _context;

        public UserLikeRepository(DatingAppContext context)
        {
            _context = context;
        }


        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.UserLikes
                .FirstOrDefaultAsync(p => p.SourceUserId == sourceUserId && p.LikedUserId == likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            var users = _context.Users.OrderBy(u => u.Name)
                .AsQueryable();

            var likes = _context.UserLikes.AsQueryable();

            if (predicate == "liked")
            {

                likes = likes.Where(p => p.SourceUserId == userId);
                users = likes.Select(p =>p.LikedUser);

            }

            if (predicate == "likedBy")
            {
                likes = likes.Where(p => p.LikedUserId == userId);
                users = likes.Select(p => p.SourceUser);
            }

            return await users.Select(user => new LikeDto()
            {
                Username = user.Name,
                KnownAs = user.KnowAs,
                DateOfBirth = user.DateOfBirth,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                Id = user.UserID
            }).ToListAsync();
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(l => l.LikedUsers)
                .FirstOrDefaultAsync(l => l.UserID == userId);
        }

        public async Task InsertAsync(UserLike userLike)
        {
            await _context.UserLikes.AddAsync(userLike);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
