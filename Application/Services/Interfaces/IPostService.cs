using Domain.DTOs.Post;
using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IPostService
    {
        Task InsertPost(PostDTO postDTO,int userId);
        Task Delete(int id);

    }
}
