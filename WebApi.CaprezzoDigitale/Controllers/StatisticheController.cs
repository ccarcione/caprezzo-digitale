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

        [HttpPost("AperturaApp")]
        public void AperturaApp()
        {
            _context.Statistiche.Add(new Statistica()
            {
                TipoStatisticaId = (int)TipiStatistica.AperturaApp,
                Data = DateTime.Now,
                Valore = "1"
            });
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException();            
            }
        }

        [HttpPost("InstallazioneApp")]
        public void InstallazioneApp()
        {
            _context.Statistiche.Add(new Statistica()
            {
                TipoStatisticaId = (int)TipiStatistica.InstallazioneApp,
                Data = DateTime.Now,
                Valore = "1"
            });
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DatabaseLayerException("Errore nel salvataggio delle statistiche", e);
            }
        }
    }
}
