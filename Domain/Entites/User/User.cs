using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.User
{
    public class User
    {
        //user properties
        #region properties

        [Key]
        public int UserID { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "حداکثر کارکتر مجاز {1} میباشد  ")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "حداکثر کارکتر مجاز {1} میباشد  ")]
        public string password { get; set; }
        [Display(Name = "نام کاربری")]
        [MaxLength(350, ErrorMessage = "حداکثر کارکتر مجاز {1} میباشد  ")]
        public string? Name { get; set; }
        [Display(Name = "شماره موبایل")]
        [MaxLength(11, ErrorMessage = "حداکثر کارکتر مجاز {1} میباشد  ")]

        public string? Mobile { get; set; }
        [Display(Name = "آواتار")]
        [MaxLength(50, ErrorMessage = "حداکثر کارکتر مجاز {1} میباشد  ")]
        public string? Avatar { get; set; }
        [Display(Name = "ایمیل فعال است؟")]
        public bool IsEmailActive { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Display(Name = "آخرین بازید")]
        public DateTime LastActive { get; set; } = DateTime.Now;
        [Display(Name = "جنسیت")]
        [MaxLength(350, ErrorMessage = "حداکثر کاراکتر مجاز {1} می باشد")]
        public string? Gender { get; set; }

        public string? Introduction { get; set; }

        public string? LookingFor { get; set; }

        public string? Interests { get; set; }

        [Display(Name = "شهر")]
        [MaxLength(200, ErrorMessage = "حداکثر کاراکتر مجاز {1} می باشد")]
        public string? City { get; set; }

        [Display(Name = "کشور")]
        [MaxLength(200, ErrorMessage = "حداکثر کاراکتر مجاز {1} می باشد")]
        public string? Country { get; set; }

        [Display(Name = "نحوه آشنایی")]
        [MaxLength(350, ErrorMessage = "حداکثر کاراکتر مجاز {1} می باشد")]
        public string? KnowAs { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        // **فیلدهای جدید برای فعال‌سازی ایمیل**
        [Display(Name = "توکن فعال‌سازی ایمیل")]
        public string? EmailActivationCode { get; set; }

        [Display(Name = "زمان انقضای توکن فعال‌سازی")]
        public DateTime? ActivationCodeExpireDate { get; set; }
        [Display(Name = "توکن بازنشانی رمز عبور")]
        public string? PasswordRecoveryCode { get; set; } 

        [Display(Name = "زمان انقضای توکن بازنشانی")]
        public DateTime? PasswordRecoveryCodeExpireDate { get; set; } 
        #endregion

        #region Relation

        public ICollection<Photo.Photo> Photos { get; set; }

        [InverseProperty("SourceUser")]
        public ICollection<UserLike> LikedByUsers { get; set; }

        [InverseProperty("LikedUser")]
        public ICollection<UserLike> LikedUsers { get; set; }
        #endregion
    }
}
