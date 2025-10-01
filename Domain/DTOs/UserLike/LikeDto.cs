using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.UserLike
{
    public class LikeDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime DateOfBirth { get; set; } 

        public string KnownAs { get; set; }

        public string PhotoUrl { get; set; }

        public string City { get; set; }
    }
}
