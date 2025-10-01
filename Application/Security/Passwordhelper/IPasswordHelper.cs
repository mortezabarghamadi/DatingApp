using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security.Passwordhelper
{
    public interface IPasswordHelper
    {
        #region encryption

        string EncodePasswordMd5(string password);

        #endregion
    }
}
