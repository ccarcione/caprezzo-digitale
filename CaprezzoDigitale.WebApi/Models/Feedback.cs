namespace CaprezzoDigitale.WebApi.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public string Messaggio { get; set; }
        public short Rating { get; set; }
    }
}
