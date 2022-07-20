using CaprezzoDigitale.WebApi.ApiKeyAuthorization;
using EmailTools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CaprezzoDigitale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly ETools _emailTools;
        private readonly Models.Options _options;
        public LogController(ILogger<LogController> logger, ETools emailTools, Models.Options options)
        {
            _emailTools = emailTools;
            _options = options;
            _logger = logger;
        }

        [HttpPost("Information")]
        public ActionResult Information([FromBody] JObject clientInformation)
        {
            _emailTools.SendEmailAsync(
                _options.EmailDestinatariLog,
                "Log Information - Caprezzo Digitale",
                clientInformation.ToString()
            );
            _logger.LogInformation(message: clientInformation.ToString());
            return Ok();
        }

        [HttpPost("Debug")]
        public ActionResult Debug([FromBody] JObject clientDebug)
        {
            _emailTools.SendEmailAsync(
                _options.EmailDestinatariLog,
                "Log Debug - Caprezzo Digitale",
                clientDebug.ToString()
            );
            _logger.LogDebug(message: clientDebug.ToString());
            return Ok();
        }

        [HttpPost("Trace")]
        public ActionResult Trace([FromBody] JObject clientTrace)
        {
            _emailTools.SendEmailAsync(
                _options.EmailDestinatariLog,
                "Log Trace - Caprezzo Digitale",
                clientTrace.ToString()
            );
            _logger.LogTrace(message: clientTrace.ToString());
            return Ok();
        }

        [HttpPost("Critical")]
        public ActionResult Critical([FromBody] JObject clientCritical)
        {
            _emailTools.SendEmailAsync(
                _options.EmailDestinatariLog,
                "Log Critical - Caprezzo Digitale",
                clientCritical.ToString()
            );
            _logger.LogCritical(message: clientCritical.ToString());
            return Ok();
        }

        [HttpPost("Warning")]
        public ActionResult Warning([FromBody] JObject clientWarning)
        {
            _emailTools.SendEmailAsync(
                _options.EmailDestinatariLog,
                "Log Warning - Caprezzo Digitale",
                clientWarning.ToString()
            );
            _logger.LogWarning(message: clientWarning.ToString());
            return Ok();
        }

        [HttpPost("Error")]
        public ActionResult Error([FromBody] JObject clientError)
        {
            _emailTools.SendEmailAsync(
                _options.EmailDestinatariLog,
                "Log Error - Caprezzo Digitale",
                clientError.ToString()
            );
            _logger.LogError(message: clientError.ToString());
            return Ok();
        }
    }
}
