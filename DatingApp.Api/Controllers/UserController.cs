using Application.Extensions.Common;
using Application.Services.Interfaces;
using DatingApp.Api.Extensions;
using Domain.DTOs.User;
using Domain.Entites.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatingApp.Api.Controllers
{
    
    public class UserController : BaseSiteController
    {

        #region Constructor

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService=userService;
        }

        #endregion
        //گرفتن همه کاربران
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUserInformationAsync();

            return Ok(users);
        }


        //گرفتن کاربر با نام حاص
        [HttpGet("{Name}")]
        public async Task<IActionResult> Get(string Name)
        {
            return Ok(await _userService.GetUserInformationByNameAsync(Name));
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //آپدیت اطلاعات کاربر 
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMemberDTO model)
        {
            var userId = User.GetUserId();
            var result = await _userService.UpdateMemberAsync(model, userId);
            if (result)
            {
                return new JsonResult(new
                {
                    Message = "updated",
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

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
