using PixMicroservice.Events;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using PixMicroservice.DAO;
using Google.Api;
using System.Transactions;
using PixMicroservice.Sagas;


namespace PixMicroservice.EventHandlers
{
    public class PaymentInitiatedEventHandler
    {
        private readonly IModel _channel;
        public readonly IPixContext _context;
        private readonly PaymentSaga _paymentSaga;

        public PaymentInitiatedEventHandler(IModel channel  , IPixContext pixContext , PaymentSaga paymentSaga)
        {
            _channel = channel;
            _context = pixContext;
            _paymentSaga = paymentSaga;
           

            // Declarar a fila novamente para garantir que ela existe
            _channel.QueueDeclare(queue: "payment",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Handle(PixTransaction paymentEvent)
        {
            try
            {
                ML mL = new ML();
                var teste = mL.Pix(paymentEvent);
                var qrcode = teste.PointOfInteraction.TransactionData.QrCodeBase64;
                paymentEvent.Id = Guid.NewGuid().ToString();
                _context.InsertTransactionAsync(paymentEvent);
                _paymentSaga.StartSagaNextstep(new PaymentInitiatedEvent { AccountFrom = paymentEvent.cpf, AccountTo = "Burger" , Amount = paymentEvent.Amount  , TransactionId  = paymentEvent.Id.ToString() , Timestamp= new DateTime()});

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            // Lógica para processar o evento
        }

        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var paymentEvent = JsonConvert.DeserializeObject<PixTransaction>(message);
                Handle(paymentEvent);
            };
            _channel.BasicConsume(queue: "payment", autoAck: true, consumer: consumer);
        }
    }
}
