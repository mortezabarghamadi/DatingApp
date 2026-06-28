using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.User
{
    public class UserSearchParams
    {
        public string? Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; } 
        public string? CIty { get; set; }

    }
}
