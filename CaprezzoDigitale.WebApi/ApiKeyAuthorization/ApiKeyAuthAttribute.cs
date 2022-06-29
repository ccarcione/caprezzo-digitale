using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Security.Cryptography;
using CaprezzoDigitale.WebApi.ExtensionMethods;

namespace CaprezzoDigitale.WebApi.ApiKeyAuthorization
{
    /// <summary>
    /// In a similar way to how explained in the link, an authentication system based on apikey-hash512
    /// has been created in order to protect the endpoints for authorized clients only.
    /// All this to try to simplify the situation and not set up a complete authentication system
    /// (since for now there are no users present)
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

            if (!(env.IsDevelopment() || env.IsDockerLocal()))
            {
                ApiKeyAuthOptions apiKeyAuthOptions = options.ApiKeyAuthOptions
                    .Where(a => a.ClientName == potentialClientName)
                    .FirstOrDefault();
                if (apiKeyAuthOptions == null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                if (!Get512Hash(potentialDate + potentialClientName, apiKeyAuthOptions.ApiKey).Equals(potentialSecret))
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
