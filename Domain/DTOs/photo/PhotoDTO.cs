using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.photo
{
    //PhotoTable
    public class PhotoDTO
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        #endregion
    }
}
