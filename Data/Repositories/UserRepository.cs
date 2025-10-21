using Data.Context;
using Domain.DTOs.Account;
using Domain.Entites.User;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    //repository implement
    public class UserRepository : IUserRepository
    {
        #region Constructor

        //
        private readonly DatingAppContext _context;

        public UserRepository(DatingAppContext context)
        {
            _context = context;
        }



        #endregion

        #region User

        //Get data.
        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _context.Users
                .Include(u => u.Photos)
                .ToListAsync();
        public IQueryable<User> GetAllUsersAsQueryable()
        {
            return _context.Users
                .Include(u => u.Photos)
                .AsQueryable();
        }
    

    public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.UserID == id);
        }
        //check email
        public async Task<bool> checkEmailisExist(string email)
        {
            return await _context.Users.AnyAsync(p => p.Email == email);
        }
        public async Task<User?> GetUserInformationByNameAsync(string Name)
        {
            var user = await _context.Users
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(u => EF.Functions.Like(u.Name, $"%{Name.Trim()}%"));

            return user;
        }
        public async Task<User> checkUserByEmailAndPassword(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.password == password);
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == Email);
        }
        public async Task InsertUsersAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public void DeleteUser(User user) 
        {
            _context.Users.Remove(user);
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
        #endregion
        #region SaveChange



        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }



        #endregion

    }
}
