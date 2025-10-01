using Domain.DTOs.Account;
using Domain.DTOs.Common;
using Domain.DTOs.User;
using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        #region Account
        //ثبت کاربر
        Task<RegisterDTO.Registerresult> RegisterUserAsync(RegisterDTO registerDto);
        //وارد شدن کاربر
        Task<LoginDTO.LoginResult> LoginUsersAsync(LoginDTO loginDto);


        #endregion

        #region User 
        //گرفتن اطلاعات
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetByIdAsync(int id);   
        Task<User?> GetUserByEmail(string Email);
        Task<IEnumerable<MemberDTO>> GetAllUserInformationAsync();
        Task<MemberDTO?> GetUserInformationByNameAsync(string Name);
        //آپدیت اطلاعات کاربر
        Task<bool> UpdateMemberAsync(UpdateMemberDTO model, int userId);


        #endregion
    }
}
