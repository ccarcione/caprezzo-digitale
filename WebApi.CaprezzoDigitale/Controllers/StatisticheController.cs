using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.CaprezzoDigitale.Filters;
using WebApi.CaprezzoDigitale.Middleware;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
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
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException("Errore nel salvataggio delle statistiche.", e);
            }
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
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException("Errore nel salvataggio delle statistiche.", e);
            }
        }
    }
}
