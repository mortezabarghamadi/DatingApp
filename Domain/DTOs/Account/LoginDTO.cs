using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Account
{
    public class LoginDTO
    {

        #region Properties

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "حداکثر کاراکتر مجاز {1} می‌باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "حداکثر کاراکتر مجاز {1} می‌باشد")]
        public string Password { get; set; }

        #endregion
        public enum LoginResult
        {
            Success,
            Error,
            Usernotfund,
            EmailIsNotActive
        }

    }
}
