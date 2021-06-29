namespace WebApi.CaprezzoDigitale.Models
{
    public class EmailFeedback
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Messaggio { get; set; }
        public short Rating { get; set; }
    }
}
