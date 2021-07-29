using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.CaprezzoDigitale.Filters;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class MessaggioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Options options;

        public MessaggioController(ApplicationDbContext context, Models.Options options)
        {
            _context = context;
            this.options = options;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Messaggio>>> GetMessaggi()
        {
            List<Messaggio> messaggi = await _context.Messaggi
                .Include(i => i.TipoMessaggio)
                .Include(i => i.Allegati)
                .OrderByDescending(o => o.DataPubblicazione)
                .ToListAsync();
            messaggi
                .Where(w => !string.IsNullOrWhiteSpace(w.UrlImmagineCopertina))
                .ToList()
                .ForEach(f => BuildUrlCopertina(f));
            messaggi.ForEach(m => m.Allegati.ForEach(f => BuildUrlAllegato(f, m.Id) ));

            return Ok(messaggi);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Messaggio>> GetMessaggio(long id)
        {
            Messaggio messaggio = await _context.Messaggi
                .Where(w => w.Id == id)
                .Include(i => i.TipoMessaggio)
                .Include(i => i.Allegati)
                .FirstOrDefaultAsync();

            if (messaggio == null)
            {
                return NotFound();
            }
            BuildUrlCopertina(messaggio);

            return Ok(messaggio);
        }

        private void BuildUrlCopertina(Messaggio messaggio)
        {
            messaggio.UrlImmagineCopertina = $"{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{messaggio.Id}/{messaggio.UrlImmagineCopertina}";
            messaggio.UrlPdfImmagineCopertina = $"{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{messaggio.Id}/{messaggio.UrlPdfImmagineCopertina}";
        }
        
        private void BuildUrlAllegato(Allegato allegato, long messageId)
        {
            allegato.FilePath = $"{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{messageId}/{allegato.FilePath}";
        }
    }
}
