using System;
using System.Collections.Generic;

namespace WebApi.CaprezzoDigitale.Models
{
    public class Messaggio
    {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public string Sottotitolo { get; set; }
        public DateTime DataPubblicazione { get; set; }
        /// <summary>
        /// In realtà questa property DEVE CONTENERE solo il nome del file (eventualmente sotto-directory).
        /// Il link completo alla risorsa viene generato dinamicamente durante la sua richiesta.
        /// </summary>
        public string UrlImmagine { get; set; }
        public string Testo { get; set; }
        public short TipoMessaggioId { get; set; }
        public TipoMessaggio TipoMessaggio { get; set; }
        public List<Allegato> Allegati { get; set; }
    }
}
