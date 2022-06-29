﻿using CaprezzoDigitale.WebApi.ApiKeyAuthorization;

namespace CaprezzoDigitale.WebApi.Models
{
    public class Options
    {
        public Dictionary<string, string> WebApiOptions { get; set; }
        public IEnumerable<string> EmailDestinatariFeedback { get; set; }
        public IEnumerable<string> EmailDestinatariLog { get; set; }
        public IEnumerable<ApiKeyAuthOptions> ApiKeyAuthOptions { get; set; }
    }
}
