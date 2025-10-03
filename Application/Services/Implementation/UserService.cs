using Application.Convertors;
using Application.Extensions.Common;
using Application.Security.Passwordhelper;
using Application.Security.Passwordhelper;
using Application.Senders;
using Application.Services.Interfaces;
using Domain.DTOs.Account;
using Domain.DTOs.Common;
using Domain.DTOs.photo;
using Domain.DTOs.User;
using Domain.Entites.User;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Domain.DTOs.Account.LoginDTO;
using static Domain.DTOs.Account.RegisterDTO;

namespace Application.Services.Implementation
{
    public class UserService : IUserService
    {

        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IViewRender _viewRender;
        private readonly ISendMail _sendMail;

        public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper, IViewRender viewRender, ISendMail sendMail)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _viewRender = viewRender;
            _sendMail = sendMail;
        }

        #endregion

        #region Account
        public async Task<Registerresult> RegisterUserAsync(RegisterDTO registerDTO)
        {
            #region Validations
            if (await _userRepository.checkEmailisExist(registerDTO.Email))
                return Registerresult.EmailIsExist;
            #endregion

            #region Set properties
            User user = new User()
            {
                Email = registerDTO.Email.ToLower().Trim(),
                Avatar = "Default.png",
                IsEmailActive = false, // پیش‌فرض غیرفعال
                Mobile = null,
                password = _passwordHelper.EncodePasswordMd5(registerDTO.Password),
                RegisterDate = DateTime.Now,
                Name = registerDTO.Email.Split('@')[0],
                City = registerDTO.City,
                Country = registerDTO.Country,
                DateOfBirth = registerDTO.DateOfBirth,
                Gender = registerDTO.Gender,
                KnowAs = registerDTO.KnownAs
                // EmailActivationCode و ActivationCodeExpireDate فعلاً null هستند
            };
            #endregion

            #region Insert user
            await _userRepository.InsertUsersAsync(user);
            await _userRepository.SaveChangeAsync(); 
            #endregion

            #region Send Activation Email
            // تولید توکن و ارسال ایمیل فعال‌سازی
            bool emailSent = await SendActivationEmailAsync(user);
            if (!emailSent)
            {
                // در صورت شکست در ارسال ایمیل، می‌توانید خطا برگردانید یا کاربر را پاک کنید
                return Registerresult.Error;
            }

            // ذخیره مجدد در صورت به‌روزرسانی فیلدهای توکن و تاریخ انقضا
            await _userRepository.SaveChangeAsync();
            #endregion

            return Registerresult.Success;
        }

        public async Task<LoginResult> LoginUsersAsync(LoginDTO loginDTO)
        {
            string hashPassword = _passwordHelper.EncodePasswordMd5(loginDTO.Password);

            User? user = await _userRepository.checkUserByEmailAndPassword(loginDTO.Email.ToLower().Trim(), hashPassword);

            #region Validations

            if (user is null)
                return LoginResult.Usernotfund;

            if (!user.IsEmailActive)
                return LoginResult.EmailIsNotActive;

            #endregion

            return LoginResult.Success;
        }

        #endregion

        #region User

        #region Get

        public async Task<IEnumerable<User>> GetAllUserAsync()
            => await _userRepository.GetAllUsersAsync();


        public async Task<User?> GetByIdAsync(int userId)
            => await _userRepository.GetByIdAsync(userId);

        public async Task<User?> GetUserByEmail(string email)
            => await _userRepository.GetUserByEmail(email.ToLower());

        public async Task<IEnumerable<MemberDTO>> GetAllUserInformationAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var memberDTOs = users.Select(user => new MemberDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Name = user.Name,
                Mobile = user.Mobile,
                IsEmailActive = user.IsEmailActive,
                Age = user.DateOfBirth.CalculateAge(),
                Gender = user.Gender,
                Introduction = user.Introduction,
                LookingFor = user.LookingFor,
                Interests = user.Interests,
                City = user.City,
                Country = user.Country,
                KnowAs = user.KnowAs,
                RegisterDate = user.RegisterDate,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                Photos = user.Photos.Select(p => new PhotoDTO
                {
                    Id = p.Id,
                    IsMain = p.IsMain,
                    Url = p.Url
                }).ToList()
            }).ToList();

            return memberDTOs;
        }

        public async Task<MemberDTO> GetUserInformationByNameAsync(string userName)
        {
            var user = await _userRepository.GetUserInformationByNameAsync(userName);

            if (user == null)
                return null;

            return new MemberDTO()
            {
                UserID = user.UserID,
                PhotoUrl = $"https://localhost:7220/images/users/{user.Avatar}",
                Age = user.DateOfBirth.CalculateAge(),
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                Gender = user.Gender,
                Interests = user.Interests,
                Introduction = user.Introduction,
                IsEmailActive = user.IsEmailActive,
                KnowAs = user.KnowAs,
                LookingFor = user.LookingFor,
                Mobile = user.Mobile,
                RegisterDate = user.RegisterDate,
                Name = user.Name,
                Photos = user.Photos.Select(p => new PhotoDTO()
                {
                    Id = p.Id,
                    IsMain = p.IsMain,
                    Url = p.Url
                }).ToList()
            };
        }

        #endregion

        #region ActiveAccount

        // ارسال ایمیل فعال‌سازی
        public async Task<bool> SendActivationEmailAsync(User user)
        {
            user.EmailActivationCode = Guid.NewGuid().ToString().Replace("-", "");
            user.ActivationCodeExpireDate = DateTime.Now.AddHours(24); // انقضا 24 ساعته

            _userRepository.UpdateUser(user);
            
            try
            {
                
                string baseUrl = "https://localhost:7226"; 
                string activationLink = $"{baseUrl}/api/Account/ActivateAccount?token={user.EmailActivationCode}";

                //  رندر کردن بدنه ایمیل از روی View
                // مدل ارسالی به VerifyRegisterAccount.cshtml همان لینک فعال‌سازی (string) است.
                string body = _viewRender.RenderToStringAsync("VerifyRegisterAccount", activationLink);

                // ۵. ارسال ایمیل
                _sendMail.Send(user.Email, "فعال‌سازی حساب کاربری در Dating App", body);

                return true;
            }
            catch (Exception ex)
            {
                // TODO: Log exception (استثنا را ثبت کنید)
                Console.WriteLine($"Error sending activation email: {ex.Message}");
                return false;
            }
        }



        public async Task<bool> ActivateUserByToken(string token)
        {
            var user = await _userRepository.GetAllUsersAsQueryable()
                .SingleOrDefaultAsync(u => u.EmailActivationCode == token);

            if (user == null)
            {
                return false; // توکن نامعتبر
            }

            if (user.ActivationCodeExpireDate.HasValue && user.ActivationCodeExpireDate < DateTime.Now)
            {
                user.EmailActivationCode = null;
                user.ActivationCodeExpireDate = null;
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChangeAsync();
                return false;
            }

            user.IsEmailActive = true;

            user.EmailActivationCode = null;
            user.ActivationCodeExpireDate = null;

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangeAsync();

            return true;
        }

        #endregion

        #region DeleteAccount

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            _userRepository.DeleteUser(user);
            await _userRepository.SaveChangeAsync();

            return true;
        }

        #endregion

        #region UpDateAccount

        public async Task<bool> UpdateMemberAsync(UpdateMemberDTO model, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                return false;
            user.Name=model.Name;
            user.Introduction = model.Introduction;
            user.DateOfBirth=model.DateOfBirth;
            user.City = model.City;
            user.Country = model.Country;
            user.Gender=model.Gender;
            user.Interests = model.Intrests;
            user.LookingFor = model.LookingFor;

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangeAsync();

            return true;
        }

        #endregion

        #endregion
    }
}
