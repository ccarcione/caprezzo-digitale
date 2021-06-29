using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.Filters
{
    /// <summary>
    /// In modo simile a come spiegato nel link è stato creato un sistema di autenticazione
    /// basato su apikey-hash512 così da proteggere gli endpoint per i soli client autorizzati.
    /// Tutto questo per cercare di semplificare la situazione e non configurare un sistema di autenticazione completo
    /// (dato che per ora non sono presenze utenze)
    /// https://medium.com/@zarkopafilis/asp-net-core-2-2-3-resti-api-24-setting-up-apikey-based-authentication-94169a051a5c
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var env = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            var options = context.HttpContext.RequestServices.GetRequiredService<Models.Options>();

            string potentialSecret = context.HttpContext.Request.Headers["Client-Secret"];
            string potentialDate = context.HttpContext.Request.Headers["Client-Date"];
            string potentialClientName = context.HttpContext.Request.Headers["Client-Name"];

            if (!env.IsDevelopment())
            {
                ApiKeyAuth apiKeyAuth = options.ApiKeyAuth.Where(a => a.ClientName == potentialClientName).FirstOrDefault();
                if (apiKeyAuth == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                if (!Get512Hash(potentialDate + potentialClientName, apiKeyAuth.ApiKey).Equals(potentialSecret))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }

            await next();
        }

        private static String Get256Hash(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private static String Get512Hash(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA512 hash = new HMACSHA512(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
