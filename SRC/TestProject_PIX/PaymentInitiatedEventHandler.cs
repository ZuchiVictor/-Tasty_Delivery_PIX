// src/EventHandlers/PaymentInitiatedEventHandler.cs
using Newtonsoft.Json;
using PixMicroservice.Events;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

public class PaymentInitiatedEventHandler
{
    private readonly IModel _channel;

    public PaymentInitiatedEventHandler(IModel channel)
    {
        _channel = channel;

        // Declarar a fila para garantir que ela existe
        _channel.QueueDeclare(queue: "payment",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void Handle(PaymentInitiatedEvent paymentEvent)
    {
        // Lógica para processar o evento
    }

    public void Start()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var paymentEvent = JsonConvert.DeserializeObject<PaymentInitiatedEvent>(message);
            Handle(paymentEvent);
        };
        _channel.BasicConsume(queue: "payment", autoAck: true, consumer: consumer);
    }

    // Método auxiliar para consumo manual durante o teste
    public void ProcessMessage(ReadOnlyMemory<byte> body)
    {
        var message = Encoding.UTF8.GetString(body.ToArray());
        var paymentEvent = JsonConvert.DeserializeObject<PaymentInitiatedEvent>(message);
        Handle(paymentEvent);
    }
}
