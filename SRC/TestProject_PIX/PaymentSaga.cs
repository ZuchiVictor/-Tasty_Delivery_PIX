using Moq;
using Newtonsoft.Json;
using PixMicroservice.Events;
using PixMicroservice.Sagas;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_PIX
{
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
                It.Is<byte[]>(body => Encoding.UTF8.GetString(body) == JsonConvert.SerializeObject(paymentEvent))),
                Times.Once);
        }
    }
}
