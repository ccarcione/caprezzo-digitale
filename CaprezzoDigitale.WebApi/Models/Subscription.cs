namespace CaprezzoDigitale.WebApi.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string ExpirationTime { get; set; }
        public Keys Keys { get; set; }
    }

    public class Keys
    {
        public int Id { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
        public int SubscriptionId { get; set; }
    }
}
