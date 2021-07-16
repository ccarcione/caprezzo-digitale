using System;
using System.Collections.Generic;

namespace WebApi.CaprezzoDigitale.Models
{
    public class Statistica
    {
        public long Id { get; set; }
        public string Guid { get; set; }
        public short TipoStatisticaId { get; set; }
        public TipoStatistica TipoStatistica { get; set; }
        public string Valore { get; set; }
        public DateTime Data { get; set; }
    }

    public class TipoStatistica
    {
        public short Id { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public List<Statistica> Statistiche { get; set; }
    }
}
