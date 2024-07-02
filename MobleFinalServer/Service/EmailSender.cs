using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Hosting;

namespace MobleFinalServer.Service
{
    public class EmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly SmtpClient _smtpClient;
        private readonly string _email;
        public EmailSender(string email, string pw, ILogger<EmailSender> logger)
        {
            _logger = logger;
            _email = email;
            _smtpClient = new SmtpClient("smtp.naver.com", 587)
            {
                Credentials = new NetworkCredential(email, pw),
                EnableSsl = true, // 필요한 경우 SSL 사용
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
        }

        public async Task SendEmailAsync(string email, string message)
        {
            _logger.LogInformation(_email);
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_email),
                Subject = "[One Group] 회원가입 본인인증",
                Body = "One Group 서비스에 가입하신 것을 환영합니다.<br>아래의 링크로 이동하여 인증을 완료해주세요<br>" + message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
