using MailKit.Net.Smtp; 
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Net;
using static Masar_Backend_v1.Controllers.EmailController;

namespace Masar_Backend_v1.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(ContactMessageDto dto);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) => _config = config;

        public async Task SendEmailAsync(ContactMessageDto dto)
        {
            string subject = "رسالة جديدة من نموذج الاتصال - MASAR";
            var email = new MimeMessage();
            // تأكد إن "Email" موجود في appsettings تحت "EmailSettings"
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:Email"]));
            email.To.Add(MailboxAddress.Parse(_config["EmailSettings:Email"]));
            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $@"
                    <h2>New Contact Form Submission - MASAR</h2>

                    <p><b>Name:</b> {WebUtility.HtmlEncode(dto.Name)}</p>
                    <p><b>Email:</b> {WebUtility.HtmlEncode(dto.Email)}</p>

                    <hr/>

                    <p><b>Message:</b></p>
                    <p>{WebUtility.HtmlEncode(dto.Message).Replace("\n", "<br/>")}</p>
                "
            };

            // هنا الـ SmtpClient هيتاخد من MailKit.Net.Smtp تلقائياً
            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _config["EmailSettings:Host"],
                int.Parse(_config["EmailSettings:Port"]),
                SecureSocketOptions.StartTls
            );
            var password = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD");
            await smtp.AuthenticateAsync(_config["EmailSettings:Email"], password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
