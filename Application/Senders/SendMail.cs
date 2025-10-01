using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Senders
{
    //ارسال ایمیل
    public class SendMail:ISendMail
    {

        public void Send(string to, string subject, string body)
        {
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp");

                // آدرس ایمیل فرستنده و نام نمایشی او را تنظیم کنید
                mail.From = new MailAddress("email address", "Dating app");

                // افزودن گیرنده
                mail.To.Add(to);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true; // فعال کردن HTML در متن ایمیل

                smtpServer.Credentials = new NetworkCredential("email address", "password");
                smtpServer.EnableSsl = false;

                smtpServer.Send(mail);
            }
            catch (Exception e)
            {
                // TODO: Log exception instead of ignoring it
            }
        }
    }
}
