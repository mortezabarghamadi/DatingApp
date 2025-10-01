using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites.User;
using Domain.DTOs.photo;
using Domain.Entites.Photo;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
