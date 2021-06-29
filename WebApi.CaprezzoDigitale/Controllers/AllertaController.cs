using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.CaprezzoDigitale.Filters;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class AllertaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AllertaController> _logger;

        public AllertaController(ApplicationDbContext context, ILogger<AllertaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{tipo}")]
        public async Task<ActionResult<IEnumerable<Messaggio>>> GetAllerte(string tipo)
        {
            _logger.LogDebug("Recupera l'ultimo bollettino presente a sistema.");
            BollettinoArpa bollettino = _context.BollettiniArpa.OrderByDescending(o => o.Date)
                .FirstOrDefault(s => s.Tipo == tipo);
            if (bollettino == null)
            {
                return NotFound();
            }

            _logger.LogDebug("Recupero informazioni da file.");
            // leggi il file e cast into a object

            return Ok();
        }
    }
}
