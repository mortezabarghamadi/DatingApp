using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Account
{
    public class ResetPasswordDTO
    {
        #region Properties

        // توکن بازیابی که از URL ایمیل گرفته می‌شود
        [Required]
        public string Token { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "حداکثر کاراکتر مجاز {1} می‌باشد")]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "حداکثر کاراکتر مجاز {1} می‌باشد")]
        [Compare(nameof(NewPassword), ErrorMessage = "با رمز عبور وارد کرده همخوانی ندارد")]
        public string ConfirmNewPassword { get; set; }

        #endregion
    }
}
