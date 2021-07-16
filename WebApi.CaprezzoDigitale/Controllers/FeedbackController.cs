using EmailTools;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.CaprezzoDigitale.Filters;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class FeedbackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ETools _emailTools;
        private readonly Models.Options _options;

        public FeedbackController(ApplicationDbContext context,
            ETools emailTools,
            Models.Options options)
        {
            _context = context;
            _emailTools = emailTools;
            _options = options;
        }

        [HttpPost]
        public void Post([FromBody] Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            _context.SaveChanges();

            _emailTools.SendEmailAsync(
                _options.EmailDestinatariFeedback,
                "Nuovo Feedback - Caprezzo Digitale",
                @$"<b>Nome</b>: {feedback.Nome}
<br>--------------------------------<br>
<b>Messaggio</b>: {feedback.Messaggio}
<br>--------------------------------<br>
<b>Valutazione</b>: {feedback.Rating}/5
<br>--------------------------------<br>
{DateTime.Now}
<br>
");
        }
    }
}
