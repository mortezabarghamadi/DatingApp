using Application.Services.Interfaces;
using Domain.DTOs.Post;
using Domain.Entites.Post;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatingApp.Api.Controllers
{
    public class PostController : BaseSiteController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;            
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
    }
}
