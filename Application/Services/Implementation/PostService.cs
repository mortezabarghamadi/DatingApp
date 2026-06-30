using Application.Services.Interfaces;
using Domain.DTOs.Post;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Delete(int id)
        {
            var Usere = await _postRepository.GetByIdAsync(id);
            _postRepository.DeletePost(Usere);
            await _postRepository.SaveChangesAsync();

        }

        public async Task InsertPost(PostDTO postDTO, int userId)
        {
            var newPost = new Domain.Entites.Post.Post
            {
                CreateTime = DateTime.Now,
                Content= postDTO.Content,
                CreatePostUserId = userId
            };
            await _postRepository.AddAsync(newPost);
            await _postRepository.SaveChangesAsync();
        }
    }
}
