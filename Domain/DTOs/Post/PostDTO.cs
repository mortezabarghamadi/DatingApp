using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Post
{
    public class PostDTO
    {
        [Required(ErrorMessage = "متن پست الزامی است.")]
        public string Content { get; set; } = string.Empty;
    }
}
