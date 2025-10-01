using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Common
{
    public class ResponseResult
    {
        #region Properties

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        #endregion

        #region Constructor
        public ResponseResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public ResponseResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ResponseResult(bool isSuccess, string message, object data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        #endregion
    }
}
