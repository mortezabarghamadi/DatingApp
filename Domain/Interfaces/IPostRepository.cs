using Domain.Entites.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetByuserIdAsync(int userId);
        Task<Post> GetByIdAsync(int postId);
        Task<IEnumerable<Post>> GetAllAsync();
        Task AddAsync(Post post);
        void DeletePost(Post post);
        Task SaveChangesAsync();
    }
}
