using Application.Services.Interfaces;
using Domain.DTOs.Comment;
using Domain.Entites.Comment;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
             _commentRepository = commentRepository;
        }
        public async Task AddAsync(CommentDto commentDto,int userId, int postId )
        {
            var newComment = new Comment { 
            CreateTime = DateTime.Now,
            Content = commentDto.Content,
            UserId = userId,
            PostId = postId
            };
            await _commentRepository.AddAsync( newComment );
            await _commentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Comment=await _commentRepository.GetCommentsById( id );
            _commentRepository.Delete( Comment );
            await _commentRepository.SaveChangesAsync();

        }

        public async Task<IEnumerable<CommentDto>> GetPostComments(int id)
        {
            var comments = await _commentRepository.GetCommentsByPostId(id);

            return comments.Select(c => new CommentDto
            {
                Content = c.Content
            });
        }

        public async Task<IEnumerable<CommentDto>> GetUserComments(int id)
        {
            var Comment= await _commentRepository.GetCommentsByUserId( id );
            return Comment.Select(p => new CommentDto
            {
                Content = p.Content
            });
        }

       
    }
}
