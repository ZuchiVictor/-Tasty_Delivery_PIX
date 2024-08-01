using MercadoPago;
using MercadoPago.Client;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using PixMicroservice.DAO;
using PixMicroservice.MercadoPago;

public class ML : IML
{ 


    public Payment Pix(PixTransaction pix) 
    {
       

        var requestOptions = new RequestOptions();
        requestOptions.CustomHeaders.Add("x-idempotency-key", Guid.NewGuid().ToString());

        var paymentRequest = new PaymentCreateRequest
        {
            TransactionAmount = pix.Amount, // Payment amount
            Description = "tasty_delivery",
            PaymentMethodId = "pix",
            Payer = new PaymentPayerRequest
            {
                Email = pix.Email, // Payer's email
                FirstName = pix.name,
                LastName = pix.sobrenome,
                Identification = new IdentificationRequest
                {
                    Type = "CPF",
                    Number = pix.cpf // Payer's identification number
                }
            },
            NotificationUrl = "https://www.your-notification-url.com"
        };

        var client = new PaymentClient();
        Payment payment = client.CreateAsync(paymentRequest).Result;

       return payment;

    }
}


