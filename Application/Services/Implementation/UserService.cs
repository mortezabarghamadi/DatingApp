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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                IsEmailActive = false,
                Mobile = null,
                password = _passwordHelper.EncodePasswordMd5(registerDTO.Password),
                RegisterDate = DateTime.Now,
                Name = registerDTO.Email.Split('@')[0],
                City = registerDTO.City,
                Country = registerDTO.Country,
                DateOfBirth = registerDTO.DateOfBirth,
                Gender = registerDTO.Gender,
                KnowAs = registerDTO.KnownAs
            };

            #endregion

            #region Insert user

            await _userRepository.InsertUsersAsync(user);
            await _userRepository.SaveChangeAsync();

            #endregion

            #region Send email

            var body = _viewRender.RenderToStringAsync("VerifyRegisterAccount", user);
            _sendMail.Send(user.Email, "فعال سازی حساب کاربری", body);

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
                Photos = user.Photos.Select(p => new Domain.DTOs.photo.PhotoDTO()
                {
                    Id = p.Id,
                    IsMain = p.IsMain,
                    Url = p.Url
                }).ToList()
            };
        }

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
    }
}
