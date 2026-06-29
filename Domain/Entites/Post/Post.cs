using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.Post
{
    public class Post
    {
        public int Id { get; set; }
        public string Context { get; set; }
        
        public DateTime CreateTime { get; set; }
        // این ستون کلید خارجی اصلی ماست
        public int CreatePostUserId { get; set; }
        [ForeignKey(nameof(CreatePostUserId))]
        [InverseProperty("CreatePostsUser")]
        public User.User CreatePostUser { get; set; } = null!;

    }
}
