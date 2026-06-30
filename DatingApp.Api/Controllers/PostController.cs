using Application.Services.Interfaces;
using Domain.DTOs.Comment;
using Domain.DTOs.Post;
using Domain.Entites.Post;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatingApp.Api.Controllers
{
    public class PostController : BaseSiteController
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        public PostController(IPostService postService,ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] PostDTO postDTO)
        {
            if (postDTO == null)
                return BadRequest("اطلاعات پست خالی است.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("کاربر معتبر نیست.");

            await _postService.InsertPost(postDTO, userId);

            return Ok(new
            {
                Message = "پست با موفقیت ثبت شد."
            });
        }
        [HttpPost("{postId}/comment")] // آدرس اینطوری میشه: /api/post/5/comment
        public async Task<IActionResult> AddComment(int postId, [FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
                return BadRequest("اطلاعات کامنت خالی است.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // گرفتن UserId از توکن (همون روشی که برای پست انجام دادی)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("کاربر معتبر نیست.");

            // ارسال postId (که از URL گرفتیم) و userId (که از توکن گرفتیم) به سرویس
            await _commentService.AddAsync(commentDto, userId, postId);

            return Ok(new
            {
                Message = "کامنت با موفقیت ثبت شد."
            });
        }


    }
}
