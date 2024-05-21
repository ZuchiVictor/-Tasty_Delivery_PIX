namespace PixMicroservice.DAO
{
    public class PixTransaction
    {
        public string Id { get; set; }
        public string PixKey { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
