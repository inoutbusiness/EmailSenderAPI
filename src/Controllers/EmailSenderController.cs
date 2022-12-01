using EmailSender.API.DTO_s;
using EmailSender.API.Exceptions;
using EmailSender.API.Queues.Subscribers;
using EmailSender.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.API.Controllers
{
    [ApiController]
    public class EmailSenderController : Controller
    {
        private readonly IEmailSenderService _emailSenderService;

        public EmailSenderController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }


        [HttpPost]
        [Route("/api/v1/emailSender/sendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailSenderRequest request)
        {
            try
            {
                new EmailSenderSubscriber().Consume();

                var response = await _emailSenderService.SendEmailAsync(request);

                return Ok(response);
            }
            catch (EmailSenderException emailSenderEx)
            {
                return BadRequest(APIResponses.EmailSenderError(emailSenderEx.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, APIResponses.ApplicationError(ex.Message));
            }
        }
    }
}
