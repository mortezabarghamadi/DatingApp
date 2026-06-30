using Domain.Entites.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentsById(int id);
        Task<IEnumerable<Comment>> GetCommentsByUserId(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostId(int id);

        Task AddAsync(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
        Task SaveChangesAsync();
    }
}
