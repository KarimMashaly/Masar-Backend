using Resend;
using static Masar_Backend_v1.Controllers.EmailController;

namespace Masar_Backend_v1.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(ContactMessageDto dto);
    }

    public class EmailService : IEmailService
    {
        private readonly IResend _resend;

        public EmailService()
        {
            _resend = ResendClient.Create(
                Environment.GetEnvironmentVariable("RESEND_API_KEY")!
            );
        }

        public async Task SendEmailAsync(ContactMessageDto dto)
        {
            var message = new EmailMessage
            {
                From = "onboarding@resend.dev",
                To = { "themasar.platform@gmail.com" }, 
                Subject = "رسالة جديدة من نموذج الاتصال - MASAR",
                HtmlBody = $@"
                    <h2>New Contact Form Submission - MASAR</h2>
                    <p><b>Name:</b> {dto.Name}</p>
                    <p><b>Email:</b> {dto.Email}</p>
                    <hr/>
                    <p><b>Message:</b></p>
                    <p>{dto.Message.Replace("\n", "<br/>")}</p>
                "
            };

            await _resend.EmailSendAsync(message);
        }
    }
}