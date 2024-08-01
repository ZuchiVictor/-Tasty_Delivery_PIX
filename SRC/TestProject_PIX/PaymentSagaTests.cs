// TestProject/Sagas/PaymentSagaTests.cs
using Xunit;
using Moq;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using PixMicroservice.Events;
using PixMicroservice.Sagas;

public class PaymentSagaTests
{
    [Fact]
    public void PaymentSaga_Should_Publish_Event()
    {
        // Arrange
        var mockChannel = new Mock<IModel>();
        var saga = new PaymentSaga(mockChannel.Object);
        var paymentEvent = new PaymentInitiatedEvent
        {
            TransactionId = "123",
            Amount = 100.00m,
            AccountFrom = "account1",
            AccountTo = "account2",
            Timestamp = DateTime.Now
        };

        // Act
        saga.StartSaga(paymentEvent);

        // Assert
        mockChannel.Verify(m => m.BasicPublish(
            It.IsAny<string>(),
            "payment",
            It.IsAny<IBasicProperties>(),
            It.IsAny<ReadOnlyMemory<byte>>()),
            Times.Once);

        var expectedBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paymentEvent));
        mockChannel.Verify(m => m.BasicPublish(
            It.IsAny<string>(),
            "payment",
            It.IsAny<IBasicProperties>(),
            It.Is<ReadOnlyMemory<byte>>(body => body.ToArray().SequenceEqual(expectedBody))),
            Times.Once);
    }
}
