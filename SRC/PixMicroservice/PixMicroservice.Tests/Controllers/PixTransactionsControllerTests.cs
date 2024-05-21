using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using PixMicroservice.DAO;

public class PixTransactionsControllerTests
{
    [Fact]
    public async Task GetPixTransactions_ReturnsOkResult_WithListOfTransactions()
    {
        // Arrange
        var mockContext = new Mock<PixContext>(null);
        mockContext.Setup(context => context.GetAllTransactionsAsync())
            .ReturnsAsync(GetSampleTransactions());
        var controller = new PixTransactionsController(mockContext.Object);

        // Act
        var result = await controller.GetPixTransactions();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<PixTransaction>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    private List<PixTransaction> GetSampleTransactions()
    {
        return new List<PixTransaction>
        {
            new PixTransaction { Id = "1", PixKey = "key1", Amount = 100, TransactionDate = DateTime.Now },
            new PixTransaction { Id = "2", PixKey = "key2", Amount = 200, TransactionDate = DateTime.Now }
        };
    }
}
