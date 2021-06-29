using EmailLibTool;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.CaprezzoDigitale.Filters;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class EmailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSend _emailSend;
        private readonly Models.Options _options;

        public EmailController(ApplicationDbContext context,
            EmailSend emailSend,
            Models.Options options)
        {
            _context = context;
            _emailSend = emailSend;
            _options = options;
        }

        [HttpPost]
        public void Post([FromBody] EmailFeedback emailFeedback, [FromQuery] bool saveFeedback)
        {
            _emailSend.SendEmailAsync(
                _options.EmailDestinatariFeedback,
                "Nuovo Feedback - Caprezzo Digitale",
                @$"<b>Nome</b>: {emailFeedback.Nome}
<br>--------------------------------<br>
<b>Messaggio</b>: {emailFeedback.Messaggio}
<br>--------------------------------<br>
<b>Valutazione</b>: {emailFeedback.Rating}/5
<br>--------------------------------<br>
{DateTime.Now}
<br>
");
            if (saveFeedback)
            {
                _context.Feedbacks.Add(emailFeedback);
                _context.SaveChanges();
            }
        }
    }
}
