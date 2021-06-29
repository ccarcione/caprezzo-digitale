using System.Collections.Generic;

namespace WebApi.CaprezzoDigitale.Models
{
    public class TipoMessaggio
    {
        public short Id { get; set; }
        public string DisplayName { get; set; }
        public string Descrizione { get; set; }
        public string Icona { get; set; }
        public string Colore { get; set; }
        public List<Messaggio> Messaggi { get; set; }
    }
}
