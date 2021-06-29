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
            messaggi.ForEach(f => f.UrlImmagine = $"{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{f.UrlImmagine}");
            messaggi.ForEach(m => m.Allegati.ForEach(f =>
                f.FilePath = $"{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{f.FilePath}"
            ));
            return Ok(messaggi);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Messaggio>> GetMessaggio(long id)
        {
            var messaggio = await _context.Messaggi
                .Where(w => w.Id == id)
                .Include(i => i.TipoMessaggio)
                .Include(i => i.Allegati)
                .FirstOrDefaultAsync();

            if (messaggio == null)
            {
                return NotFound();
            }
            messaggio.UrlImmagine = $"{options.WebApiOptions["publicStaticFiles_RequestPath"]}/{messaggio.UrlImmagine}";

            return Ok(messaggio);
        }
    }
}
