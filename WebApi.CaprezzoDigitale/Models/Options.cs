using System.Collections.Generic;

namespace WebApi.CaprezzoDigitale.Models
{
    public class Options
    {
        public Dictionary<string, string> WebApiOptions { get; set; }
        public IEnumerable<string> EmailDestinatariFeedback { get; set; }
        public IEnumerable<string> EmailDestinatariLog { get; set; }
        public IEnumerable<ApiKeyAuth> ApiKeyAuth { get; set; }
    }
}
