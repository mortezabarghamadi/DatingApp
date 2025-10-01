using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.Common
{
    //گرفتن ایدی کاربر
    public static class IdenetityExtension
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userID = claimsPrincipal.FindFirst("UserID")?.Value;
            if (!string.IsNullOrWhiteSpace(userID))
            {
                return int.Parse(userID);
            }
            else
            {
                return default(int);
            }
        }
    }
}
