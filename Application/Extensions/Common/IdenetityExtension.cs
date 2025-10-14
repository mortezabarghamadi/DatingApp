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
            var userID = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrWhiteSpace(userID))
            {
                // تلاش برای تبدیل به عدد صحیح
                if (int.TryParse(userID, out int id))
                {
                    return id;
                }
            }
            // در صورت عدم وجود یا عدم تبدیل صحیح، 0 برگردانده شود.
            return default(int);
        }
    }
}
