using CaprezzoDigitale.WebApi.Helpers;
using CaprezzoDigitale.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CaprezzoDigitale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleriaController : ControllerBase
    {
        private readonly Options options;

        public GalleriaController(Options options)
        {
            this.options = options;
        }

        // GET: api/<GalleriaController>
        [HttpGet]
        public ActionResult<PagedList<string>> Get([FromQuery] QueryParameters queryParameters)
        {
            // get all file from PublicGalleria
            List<string> listImageFileName = Directory.GetFiles(
                    UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["mockImage_Path"]),
                    string.Empty,
                    SearchOption.TopDirectoryOnly)
                .Select(s => Path.GetFileName(s))
                .OrderBy(o => o)
                // build link
                .Select(f => f = $"{options.WebApiOptions["galleria_RequestPath"]}/{f}")
                .ToList();

            // paged elements
            PagedList<string> pagedList = PagedList<string>.ToPagedList(listImageFileName, queryParameters);

            return Ok(pagedList);
        }
    }
}
