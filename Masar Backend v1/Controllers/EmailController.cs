using Masar_Backend_v1.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Masar_Backend_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("send-message")]
        public async Task<IActionResult> SendContactMessage([FromBody] ContactMessageDto request)
        {
            // التأكد إن البيانات مبعوتة كاملة
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "بيانات الفورم غير مكتملة." });
            }

            try
            {
                await _emailService.SendEmailAsync(request);

                return Ok(new { success = true, message = "تم إرسال رسالتك بنجاح، سنقوم بالرد عليك قريباً." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "فشل في إرسال الرسالة، حاول مرة أخرى لاحقاً." });
            }
        }

        public class ContactMessageDto
        {
            [Required]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string Name { get; set; } = string.Empty;
            [Required]
            public string Message { get; set; } = string.Empty;
        }
    }
}
