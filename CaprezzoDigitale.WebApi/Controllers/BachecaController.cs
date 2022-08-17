using CaprezzoDigitale.WebApi.ApiKeyAuthorization;
using CaprezzoDigitale.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CaprezzoDigitale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class BachecaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Options options;

        public BachecaController(ApplicationDbContext context, Options options)
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
            
            BuildUrlMessaggio(messaggi);

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
            BuildUrlMessaggio(messaggio);

            return Ok(messaggio);
        }

        private void BuildUrlMessaggio(Messaggio messaggio)
        {
            BuildUrlCopertina(messaggio);
            messaggio.Allegati.ForEach(f => BuildUrlAllegato(f, messaggio.Id));
        }

        private void BuildUrlMessaggio(List<Messaggio> messaggi)
        {
            messaggi
                .ToList()
                .ForEach(f => BuildUrlCopertina(f));
            messaggi
                .ForEach(m => m.Allegati.ForEach(f => BuildUrlAllegato(f, m.Id)));
        }

        private void BuildUrlCopertina(Messaggio messaggio)
        {
            if (!string.IsNullOrWhiteSpace(messaggio.UrlImmagineCopertina))
            {
                messaggio.UrlImmagineCopertina =
                    $"{options.WebApiOptions["url"]}/{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{messaggio.UrlImmagineCopertina}";
            }
            if (!string.IsNullOrWhiteSpace(messaggio.UrlPdfImmagineCopertina))
            {
                messaggio.UrlPdfImmagineCopertina = $"{options.WebApiOptions["url"]}/{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{messaggio.UrlPdfImmagineCopertina}";
            }
        }

        private void BuildUrlAllegato(Allegato allegato, long messageId)
        {
            allegato.FilePath = $"{options.WebApiOptions["url"]}/{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{allegato.FilePath}";
        }

    }
}
