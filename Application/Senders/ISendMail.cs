using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Senders
{
    public interface ISendMail
    {
        void Send(string to, string subject, string body);
    }
}
