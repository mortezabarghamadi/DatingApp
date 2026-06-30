using Domain.Entites.Post;
using Domain.Entites.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.Comment
{
    public class Comment
    { 
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post.Post Post { get; set; } = null!;


        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User.User User { get; set; } = null!;

    }
}
