using CaprezzoDigitale.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaprezzoDigitale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticheController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatisticheController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AperturaApp/{guid}")]
        public void AperturaApp(string guid)
        {
            _context.Statistiche.Add(new Statistica()
            {
                Guid = guid,
                TipoStatisticaId = (int)TipiStatistica.AperturaApp,
                Valore = "1"
            });
            _context.SaveChanges();
        }

        [HttpPost("InstallazioneApp/{guid}")]
        public void InstallazioneApp(string guid)
        {
            _context.Statistiche.Add(new Statistica()
            {
                Guid = guid,
                TipoStatisticaId = (int)TipiStatistica.InstallazioneApp,
                Valore = "1"
            });
            _context.SaveChanges();
        }

    }
}
