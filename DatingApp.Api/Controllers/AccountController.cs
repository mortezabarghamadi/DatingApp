using Application.Services.Interfaces;
using DatingApp.Api.Services.Interfaces;
using Domain.DTOs.Account;
using Domain.DTOs.Common;
using Domain.DTOs.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.DTOs.Account.LoginDTO;
using static Domain.DTOs.Account.RegisterDTO;

namespace DatingApp.Api.Controllers
{
    public class AccountController : BaseSiteController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService,ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        #endregion
        // وارد شدن کاربر
        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid) {
                List<string> errors = new List<string>();

                foreach (var modelError in ViewData.ModelState.Values)
                foreach (var error in modelError.Errors)
                    errors.Add(error.ErrorMessage);

                return new JsonResult(new ResponseResult(false, "", errors));
            }


            LoginDTO.LoginResult res = await _userService.LoginUsersAsync(loginDto);
            switch (res)
            {
                case LoginDTO.LoginResult.Success:
                    var user = await _userService.GetUserByEmail(loginDto.Email);
                    if (user == null)
                        return new JsonResult(new ResponseResult(false, "متاسفانه حساب کاربری شما یافت نشد."));

                    return new JsonResult(new ResponseResult(true, "با موفقیت در حساب کاربری خود وارد شدید ",
                        new UserDTO
                        {
                            Name = user.Name,
                            Token = _tokenService.CreateToken(user),
                            Gender = user.Gender

                        }));
                    ; break;

                case LoginResult.Error:
                    return new JsonResult(new ResponseResult(false, "مشکلی پیش آمده است. لطفا مجدد تلاش کنید"));
                case LoginResult.EmailIsNotActive:
                    return new JsonResult(new ResponseResult(false, "حساب کاربری شما فعال نشده است. لطفا ابتدا حساب کاربری خودتون رو فعال کنید."));
                default:
                    break;
            }
            return Ok();
        }

        #endregion
        //ثبت کابر
        #region Registre
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDto)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var modelError in ViewData.ModelState.Values)
                    foreach (var error in modelError.Errors)
                        errors.Add(error.ErrorMessage);

                return new JsonResult(new ResponseResult(false, "", errors));
            }

            #endregion


            RegisterDTO.Registerresult res = await _userService.RegisterUserAsync(registerDto);

            switch (res)
            {
                case RegisterDTO.Registerresult.Success:
                    var user = await _userService.GetUserByEmail(registerDto.Email);
                    if (user == null)
                        return new JsonResult(new ResponseResult(false, "متاسفانه حساب کاربری شما یافت نشد."));

                    return new JsonResult(new ResponseResult(true, "با موفقیت حساب کاربری ساخته شد .",
                        new UserDTO
                        {
                            Name = user.Name,
                            Token = _tokenService.CreateToken(user),
                            Gender = user.Gender

                        }));

                

                case RegisterDTO.Registerresult.Error:
                    return Json(new ResponseResult(false, "خطای نامشخص در ثبت نام. لطفا دوباره تلاش کنید"));


                case RegisterDTO.Registerresult.EmailIsExist:
                    return Json(new ResponseResult(false, "آدرس ایمیل تکراریست"));


                default:
                    break;
            }
            return Ok();

        }
        #endregion

        #region Activate Account
        // فعال‌سازی حساب کاربری
        [HttpGet("ActivateAccount")]
        public async Task<IActionResult> ActivateAccount([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new ResponseResult(false, "توکن فعال‌سازی ارسال نشده است."));
            }

            var result = await _userService.ActivateUserByToken(token);

            if (result)
            {
                // می‌توانید به جای JSON یک View موفقیت‌آمیز را برگردانید یا کاربر را به صفحه ورود هدایت کنید
                return Ok(new ResponseResult(true, "حساب کاربری شما با موفقیت فعال شد. می‌توانید وارد شوید."));
            }
            else
            {
                // می‌توانید پیام‌های خطا را بر اساس دلیل شکست (منقضی یا نامعتبر) بهتر کنید
                return BadRequest(new ResponseResult(false, "فعال‌سازی حساب کاربری موفقیت‌آمیز نبود. توکن نامعتبر یا منقضی شده است."));
            }
        }
        #endregion
        //فراموشی رمز عبور
        #region Forgot Password

        //درخواست فراموشی رمز عبور
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var modelError in ViewData.ModelState.Values)
                foreach (var error in modelError.Errors)
                    errors.Add(error.ErrorMessage);

                return new JsonResult(new ResponseResult(false, "", errors));
            }

            var result = await _userService.SendPasswordRecoveryEmailAsync(forgotPassword.Email);

            return Ok(new ResponseResult(true, "اگر این ایمیل در سیستم ما ثبت شده باشد، لینک بازنشانی برای شما ارسال خواهد شد."));
        }

        //بازنشانی رمز عبور
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                foreach (var modelError in ViewData.ModelState.Values)
                foreach (var error in modelError.Errors)
                    errors.Add(error.ErrorMessage);

                return new JsonResult(new ResponseResult(false, "", errors));
            }

            var result = await _userService.ResetPasswordAsync(resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (result)
            {
                return Ok(new ResponseResult(true, "رمز عبور شما با موفقیت بازنشانی شد."));
            }
            else
            {
                return BadRequest(new ResponseResult(false, "بازنشانی رمز عبور موفقیت‌آمیز نبود. توکن نامعتبر یا منقضی شده است."));
            }
        }

        #endregion


    }
}
