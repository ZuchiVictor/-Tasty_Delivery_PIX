using Newtonsoft.Json;
using PixMicroservice.DAO;
using PixMicroservice.Events;
using RabbitMQ.Client;
using System.Text;

namespace PixMicroservice.Sagas
{
    // src/Sagas/PaymentSaga.cs
    public class PaymentSaga
    {
        private readonly IModel _channel;

        public PaymentSaga(IModel channel)
        {
            _channel = channel;
        }

        public void StartSaga(PixTransaction paymentEvent)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paymentEvent));
            PublishMessage("payment", body);
        }

        public void StartSagaNextstep(PaymentInitiatedEvent paymentEvent)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paymentEvent));
            PublishMessage("NextStep", body);
        }

        // Método separado para publicação, facilitando o teste
        public void PublishMessage(string queueName, byte[] messageBody)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: messageBody);
            var teste  = _channel.WaitForConfirms().ToString();

            
        }
    }

}
