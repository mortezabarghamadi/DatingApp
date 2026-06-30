using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites.User;
using Domain.DTOs.photo;
using Domain.Entites.Photo;
using Domain.Entites.Post;
using Domain.Entites.Comment;

namespace Data.Context
{
    public class DatingAppContext:DbContext
    {
        //اتصال به دیتابیس
        #region Constructor

        public DatingAppContext(DbContextOptions<DatingAppContext> options) : base(options)
        {
        }

       

        #endregion

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }

        #endregion

        #region Photo

        public DbSet<Photo> Photos { get; set; }

        #endregion
        #region Comment
        public DbSet<Comment> Comments { get; set; }
        #endregion
        #region Post
        public DbSet<Post> Posts { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.SourceUser)
                .WithMany(u => u.LikedUsers)
                .HasForeignKey(ul => ul.SourceUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLike>()
                .HasOne(ul => ul.LikedUser)
                .WithMany(u => u.LikedByUsers)
                .HasForeignKey(ul => ul.LikedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.CreatePostUser)
                .WithMany(u => u.CreatePostsUser)
                .HasForeignKey(p => p.CreatePostUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
