using PixMicroservice.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject_PIX
{
    public class PaymentInitiatedEventTests
    {
        [Fact]
        public void PaymentInitiatedEvent_Should_SetProperties()
        {
            // Arrange
            var transactionId = "123";
            var amount = 100.00m;
            var accountFrom = "account1";
            var accountTo = "account2";
            var timestamp = DateTime.Now;

            // Act
            var paymentEvent = new PaymentInitiatedEvent
            {
                TransactionId = transactionId,
                Amount = amount,
                AccountFrom = accountFrom,
                AccountTo = accountTo,
                Timestamp = timestamp
            };

            // Assert
            Assert.Equal(transactionId, paymentEvent.TransactionId);
            Assert.Equal(amount, paymentEvent.Amount);
            Assert.Equal(accountFrom, paymentEvent.AccountFrom);
            Assert.Equal(accountTo, paymentEvent.AccountTo);
            Assert.Equal(timestamp, paymentEvent.Timestamp);
        }
    }
}
