using Data.Context;
using Domain.Entites.Post;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatingAppContext _appContext;
        public PostRepository(DatingAppContext datingApp)
        {
            _appContext = datingApp;
        }
        public async Task AddAsync(Post post)
        {
            await _appContext.Posts.AddAsync(post);
        }

        public void DeletePost(Post post)
        {
            _appContext.Posts.Remove(post);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _appContext.Posts.ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int postId)
        {
            return await _appContext.Posts.AsNoTracking().FirstOrDefaultAsync(p=>p.Id==postId);
        }

        public async Task<IEnumerable<Post>> GetByuserIdAsync(int userId)
        {
            return await _appContext.Posts.Include(c=>c.CreatePostUser).Where(p=>p.CreatePostUserId==userId).AsNoTracking().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }

       
    }
}
