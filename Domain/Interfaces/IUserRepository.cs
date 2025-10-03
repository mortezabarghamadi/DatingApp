using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Account;
using Domain.Entites.User;

namespace Domain.Interfaces
{
    //repository interface
    public interface IUserRepository
    {


        #region User 
        //Get data
        Task<IEnumerable<User>> GetAllUsersAsync();

        IQueryable<User> GetAllUsersAsQueryable();
        Task<User?> GetByIdAsync(int id);
        Task<User> checkUserByEmailAndPassword( string email,string password);
        Task<User?> GetUserByEmail(string Email);
        Task<User?> GetUserInformationByNameAsync(string Name);
        //چک کردن عدم وارد کردن ایمیل تکراری
        Task<bool> checkEmailisExist(string email);

        //آپدیت اطلاعات
        void UpdateUser(User user);
        //حذف اکانت
        void DeleteUser(User user); 
        //اضافه کردن تغییرات
        Task InsertUsersAsync(User user);


        #endregion

        #region SaveChange
        //ذخیره اطلاعات
        Task SaveChangeAsync();

        #endregion
    }
}
