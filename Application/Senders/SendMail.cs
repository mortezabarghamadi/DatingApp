
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration; 
using System.Threading.Tasks;

namespace Application.Senders
{
    //ارسال ایمیل
    public class SendMail : ISendMail
    {
        private readonly IConfiguration _configuration;

        // تزریق IConfiguration برای خواندن تنظیمات
        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string to, string subject, string body)
        {
            // خواندن تنظیمات از appsettings.json
            var host = _configuration["EmailSettings:SmtpHost"];
            var port = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var password = _configuration["EmailSettings:Password"];
            var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);

            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient(host, port); 

                mail.From = new MailAddress(senderEmail, "Dating app");

                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                smtpServer.Credentials = new NetworkCredential(senderEmail, password);
                smtpServer.EnableSsl = enableSsl; 

                smtpServer.Send(mail);
            }
            catch (Exception e)
            {
               
                Console.WriteLine($"Email sending failed: {e.Message}");
            }
        }
    }
}