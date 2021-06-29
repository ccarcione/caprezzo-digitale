using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.CaprezzoDigitale.Filters;
using WebApi.CaprezzoDigitale.Models;
using WebAPI.NC.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.CaprezzoDigitale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class GalleriaController : ControllerBase
    {
        private readonly Options options;

        public GalleriaController(Options options)
        {
            this.options = options;
        }

        // GET: api/<GalleriaController>
        [HttpGet]
        public async Task<ActionResult<PagedList<string>>> Get([FromQuery] QueryParameters queryParameters)
        {
            // get all file from PublicGalleria
            List<string> listImageFileName = Directory.GetFiles(SeedMockData.GetWindowsLinuxPath(options.WebApiOptions["mockImage_Path"]), "", SearchOption.TopDirectoryOnly)
                .Select(s => Path.GetFileName(s))
                .OrderBy(o => o)
                // costruisci link
                .Select(f => f = $"{options.WebApiOptions["galleria_RequestPath"]}/{f}")
                .ToList();

            // pagina elementi
            PagedList<string> pagedList = PagedList<string>.ToPagedList(listImageFileName, queryParameters);

            return Ok(pagedList);
        }
    } 
}
