using CaprezzoDigitale.WebApi.ApiKeyAuthorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaprezzoDigitale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class AllertaController : ControllerBase
    {
    }
}
