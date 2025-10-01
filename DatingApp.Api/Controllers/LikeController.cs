using Application.Extensions.Common;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    public class LikeController : BaseSiteController
    {
        #region Constructor
        
        private readonly IUserLikeService _userLikeService;

        public LikeController(IUserLikeService userLikeService)
        {
            _userLikeService = userLikeService;
        }

        #endregion


        #region Add like
        //افزودن لایک

        [HttpPost("{userName}")]
        public async Task<IActionResult> AddLike(string userName)
        {
            var sourceUserId = User.GetUserId();

            var result = await _userLikeService.AddUserLikeAsync(sourceUserId, userName);
            if (result)
            {
                return new JsonResult(new
                {
                    Message = "عملیات با موفقیت انجام شد",
                    StatusCode = 200,
                    IsSuccess = true
                });
            }
            else
            {
                return new JsonResult(new
                {
                    Message = "Has error",
                    StatusCode = 201,
                    IsSuccess = false
                });
            }
        }

        #endregion

        #region Get user like
        //گرفتن لیست کاربران لایک شده یا لایک کننده
        [HttpGet]
        public async Task<IActionResult> GetUserLikes(string predicate)
        {
            var users = await _userLikeService.GetUserLikes(predicate, User.GetUserId());

            return Ok(users);
        }

        #endregion

    }
}
