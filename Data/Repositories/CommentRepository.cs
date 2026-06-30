using Data.Context;
using Domain.Entites.Comment;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatingAppContext _datingAppContext;
        public CommentRepository(DatingAppContext datingAppContext)
        {
            _datingAppContext = datingAppContext;
        }
        public async Task AddAsync(Comment comment)
        {
            await _datingAppContext.Comments.AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            _datingAppContext.Comments.Remove(comment);
        }

        public async Task<Comment> GetCommentsById(int id)
        {
            return await _datingAppContext.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(p=>p.Id== id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int id)
        {
            return await _datingAppContext.Comments
                .Include(c => c.User)
                .Where(p=>p.PostId== id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserId(int id)
        {
            return await _datingAppContext.Comments
                .Include(c=>c.User)
                .Where(p => p.UserId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _datingAppContext.SaveChangesAsync();
        }

        public void Update(Comment comment)
        {
            _datingAppContext.Comments.Update(comment);
        }
    }
}
