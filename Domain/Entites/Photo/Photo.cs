using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.Photo
{
    public class Photo
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        public string PublicId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Relations

        // **شیء ناوبری برای User**
        [ForeignKey(nameof(UserId))]
        public User.User User { get; set; }

        #endregion
    }
}
