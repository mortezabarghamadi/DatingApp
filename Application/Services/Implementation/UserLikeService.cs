using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Domain.DTOs.UserLike;
using Domain.Entites.User;
using Domain.Interfaces;

namespace Application.Services.Implementation
{
    public class UserLikeService:IUserLikeService

    {
        private readonly IUserLikeRepository _userLikeRepository;
        private readonly IUserRepository _userRepository;

        public UserLikeService(IUserLikeRepository userLikeRepository, IUserRepository userRepository)
        {
            _userLikeRepository = userLikeRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserLikeAsync(int sourceUserId, string userName)
        {
            var likedUser = await _userRepository.GetUserInformationByNameAsync(userName);
            var sourceUser = await _userRepository.GetByIdAsync(sourceUserId);


            if (likedUser == null)
                return false;

            if (sourceUser.Name == userName)
                return false;

            var userLike = await _userLikeRepository.GetUserLike(sourceUserId, likedUser.UserID);
            if (userLike != null)
                return false;

            userLike = new UserLike()
            {
                SourceUserId = sourceUser.UserID,
                LikedUserId = likedUser.UserID
            };

            await _userLikeRepository.InsertAsync(userLike);
            await _userLikeRepository.SaveAsync();

            return true;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _userLikeRepository
                .GetUserLike(sourceUserId, likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            return await _userLikeRepository.GetUserLikes(predicate, userId);
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _userLikeRepository.GetUserWithLikes(userId);
        }
    }
}
