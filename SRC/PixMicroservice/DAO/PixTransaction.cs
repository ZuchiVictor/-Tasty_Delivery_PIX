using Couchbase;
using System.Text.Json.Serialization;

namespace PixMicroservice.DAO
{
    public class PixTransaction
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("pixKey")]
        public string PixKey { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("transactionDate")]
        public DateTime TransactionDate { get; set; }
        public string name { get; set; }

        public string sobrenome { get; set; }

        public string  cpf { get; set; }
        public string Email { get; set; }
    }


    public class PixBucketContainer
    {
        public PixTransaction PixBucket { get; set; }
    }

}
