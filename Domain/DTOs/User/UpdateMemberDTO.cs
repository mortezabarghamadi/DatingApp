using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.User
{
    public class UpdateMemberDTO
    {
        #region Properties

        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Intrests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        #endregion
    }
}
