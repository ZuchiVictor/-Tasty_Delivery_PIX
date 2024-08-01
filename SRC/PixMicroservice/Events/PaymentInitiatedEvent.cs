namespace PixMicroservice.Events
{
    public class PaymentInitiatedEvent
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string AccountFrom { get; set; }
        public string AccountTo { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
