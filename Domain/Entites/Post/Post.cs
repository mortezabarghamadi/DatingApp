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
        public string Content { get; set; }
        
        public DateTime CreateTime { get; set; }
        // این ستون کلید خارجی اصلی ماست
        public int CreatePostUserId { get; set; }
        [ForeignKey(nameof(CreatePostUserId))]
        public User.User CreatePostUser { get; set; } = null!;
        public ICollection<Comment.Comment> Comments { get; set; } = new List<Comment.Comment>();
    }
}
