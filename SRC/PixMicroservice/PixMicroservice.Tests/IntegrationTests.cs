using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using PixMicroservice;
using PixMicroservice.DAO;

public class IntegrationTests 
{
    private readonly HttpClient _client;


    [Fact]
    public async Task PostPixTransaction_ReturnsCreatedResponse()
    {
        // Arrange
        var transaction = new PixTransaction
        {
            Id = "3",
            PixKey = "key3",
            Amount = 300,
            TransactionDate = DateTime.Now
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/PixTransactions", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}
