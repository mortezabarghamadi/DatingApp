using Domain.DTOs.Comment;
using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICommentService
    {

        Task<IEnumerable<CommentDto>> GetPostComments(int id);
        Task<IEnumerable<CommentDto>> GetUserComments(int id);

        Task AddAsync(CommentDto commentDto, int userId, int postId);
        Task DeleteAsync(int id);
    }
}
