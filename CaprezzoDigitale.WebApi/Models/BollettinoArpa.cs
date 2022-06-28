using System;

namespace CaprezzoDigitale.WebApi.Models
{
    public class BollettinoArpa
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Tipo { get; set; }
        public string XML_FileName { get; set; }
        public string PDF_FileName { get; set; }
    }
}
