// TestProject/EventHandlers/PaymentInitiatedEventHandlerTests.cs
using Xunit;
using Moq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using PixMicroservice.Events;

public class PaymentInitiatedEventHandlerTests
{
    [Fact]
    public void PaymentInitiatedEventHandler_Should_Handle_Event()
    {
        // Arrange
        var mockChannel = new Mock<IModel>();
        var handler = new PaymentInitiatedEventHandler(mockChannel.Object);
        var paymentEvent = new PaymentInitiatedEvent
        {
            TransactionId = "123",
            Amount = 100.00m,
            AccountFrom = "account1",
            AccountTo = "account2",
            Timestamp = DateTime.Now
        };
        var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paymentEvent));

        // Act
        handler.ProcessMessage(new ReadOnlyMemory<byte>(messageBody));

        // Assert
        // Aqui você pode adicionar verificações específicas para a lógica de processamento do evento
    }
}
