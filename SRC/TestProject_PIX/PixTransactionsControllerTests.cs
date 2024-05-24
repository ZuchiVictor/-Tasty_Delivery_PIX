using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using PixMicroservice.DAO;
using MercadoPago.Config;

namespace PixMicroservice.Tests
{
    public class PixTransactionsControllerTests
    {
        [Fact]
        public async Task GetPixTransactions_ReturnsOkResult()
        {
            // Arrange
            var mockContext = new Mock<IPixContext>();
            var transactions = new List<PixTransaction>
            {
                new PixTransaction { Id = "1", PixKey = "56874540041", Amount = 100, TransactionDate = DateTime.UtcNow }
            };
            mockContext.Setup(ctx => ctx.GetAllTransactionsAsync(It.IsAny<string>()))
                       .ReturnsAsync(transactions);
            var controller = new PixTransactionsController(mockContext.Object);

            // Act
            var result = await controller.GetPixTransactions("56874540041");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTransactions = Assert.IsAssignableFrom<IEnumerable<PixTransaction>>(okResult.Value);
            Assert.NotNull(returnedTransactions);
            Assert.Single(returnedTransactions);
        }

        [Fact]
        public async Task PostPixTransaction_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockContext = new Mock<IPixContext>();
            var transaction = new PixTransaction { PixKey = "56874540041", Amount = 100, TransactionDate = DateTime.UtcNow, Email = "teste@teste.com", name = "teste", sobrenome = "testinho", cpf = "56874540041", Id = "12" };

            // Mock the InsertTransactionAsync method to complete the task
            mockContext.Setup(ctx => ctx.InsertTransactionAsync(It.IsAny<PixTransaction>()))
                       .Returns(Task.CompletedTask);
            var controller = new PixTransactionsController(mockContext.Object);

            // Act
            MercadoPagoConfig.AccessToken = "TEST-4763904494948372-052122-142d8d655e8b9f22f5d241ad3612cf62-80196247";
            var result = await controller.PostPixTransaction(transaction);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdTransaction = Assert.IsType<PixTransaction>(createdAtActionResult.Value);
            Assert.Equal(transaction.PixKey, createdTransaction.PixKey);
        }
    }

    public class PixTransactionTests
    {
        [Fact]
        public void PixTransaction_Id_Generated()
        {
            // Arrange
            var transaction = new PixTransaction { PixKey = "56874540041", Amount = 100, TransactionDate = DateTime.UtcNow, Email = "teste@teste.com", name = "teste", sobrenome = "testinho", cpf = "56874540041", Id = "12" };

            // Assert
            Assert.NotNull(transaction.Id);
            Assert.NotEmpty(transaction.Id);
            Assert.IsType<string>(transaction.Id);
        }
    }
}
