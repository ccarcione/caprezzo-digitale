using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CaprezzoDigitale.WebApi.Models
{
    public class Allegato
    {
        public long Id { get; set; }
        public long MessaggioId { get; set; }
        public Messaggio Messaggio { get; set; }
        public string Descrizione { get; set; }
        [NotMapped]
        public string FileName => string.IsNullOrWhiteSpace(FilePath) ? null : Path.GetFileName(FilePath);
        public string FilePath { get; set; }
        [NotMapped]
        public byte[] Content { get; set; }
    }
}
