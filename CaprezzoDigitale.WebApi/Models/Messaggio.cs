namespace CaprezzoDigitale.WebApi.Models
{
    public class Messaggio
    {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public string Sottotitolo { get; set; }
        public DateTime DataPubblicazione { get; set; }
        /// <summary>
        /// Questa property DEVE CONTENERE solo il nome del file (eventualmente sotto-directory).
        /// </summary>
        public string UrlImmagineCopertina { get; set; }
        /// <summary>
        /// Al click dell'immagine di copertina viene aperto il pdf relativo.
        /// Questo per una migliore esperienza d'uso.
        /// </summary>
        public string UrlPdfImmagineCopertina { get; set; }
        public string Testo { get; set; }
        public short TipoMessaggioId { get; set; }
        public TipoMessaggio TipoMessaggio { get; set; }
        public List<Allegato> Allegati { get; set; }
    }
}
